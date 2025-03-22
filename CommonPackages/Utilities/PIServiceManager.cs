using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using log4net;

namespace CommonPackages.Utilities
{
    public class PIServiceManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PIServiceManager));

        private readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(5);
        private readonly ServiceController[] _serviceControllers = ServiceController.GetServices();

        public ServiceController Find(string serviceName)
        {
            var potentialServiceNames = new List<string>
            {
                serviceName,
                "PI-" + serviceName,
                "PI-OPCInt_" + serviceName,
            };

            Logger.Info($"Finding service name with potential names: {string.Join(", ", potentialServiceNames)}");
            foreach (var service in _serviceControllers)
            {
                foreach (var searchName in potentialServiceNames)
                {
                    if (service.ServiceName.Equals(searchName, StringComparison.OrdinalIgnoreCase))
                    {
                        Logger.Info($"Found service {serviceName} with name {service.ServiceName}");
                        return service;
                    }
                }
            }

            throw new Exception($"Cannot find service {serviceName} in the system");
        }

        public void Stop(ServiceController service, TimeSpan timeout = new TimeSpan())
        {
            var serviceName = service.ServiceName;
            Logger.Info($"Attempting to stop service {serviceName}");

            if (service.Status == ServiceControllerStatus.Stopped)
            {
                Logger.Info($"Service {serviceName} is already stopped");
                return;
            }

            Logger.Info($"Stopping service {serviceName}");
            service.Stop();

            timeout = timeout == TimeSpan.Zero ? _defaultTimeout : timeout;
            if (!WaitUntilStatus(service, ServiceControllerStatus.Stopped, timeout))
            {
                Logger.Error($"Service {serviceName} took too long to stop, force stop service by killing process");
                KillProcess(service.ServiceName);
            }

            Logger.Info($"Service {serviceName} has been stopped");
        }

        public void Stop(string serviceName, TimeSpan timeout = new TimeSpan())
        {
            var service = Find(serviceName);
            Stop(service, timeout);
        }

        public void Start(ServiceController service, TimeSpan timeout = new TimeSpan())
        {
            var serviceName = service.ServiceName;
            Logger.Info($"Attempting to start service {serviceName}");
            if (service.Status == ServiceControllerStatus.Running)
            {
                Logger.Info($"Service {serviceName} is already running");
                return;
            }

            Logger.Info($"Starting service {serviceName}");
            service.Start();

            timeout = timeout == TimeSpan.Zero ? _defaultTimeout : timeout;
            if (!WaitUntilStatus(service, ServiceControllerStatus.Running, timeout))
            {
                Logger.Error($"Service {serviceName} took too long to run");
                // KillProcessByName(service.ServiceName);
            }

            Logger.Info($"Service {serviceName} has been started");
        }

        public void Start(string serviceName, TimeSpan timeout = new TimeSpan())
        {
            var service = Find(serviceName);
            Start(service, timeout);
        }

        public bool WaitUntilStatus(ServiceController service, ServiceControllerStatus status, TimeSpan timeout)
        {
            var startTime = DateTime.Now;
            service.Refresh();

            Logger.Info(
                $"Waiting for {timeout.Milliseconds} millis until service {service.ServiceName} status = {status}");

            while (service.Status != status)
            {
                if (DateTime.Now - startTime > timeout)
                {
                    return false;
                }

                Thread.Sleep(250);
                service.Refresh();
            }

            return true;
        }

        public void KillProcess(string processName)
        {
            try
            {
                Logger.Info($"Killing process {processName}");
                var process = Process.Start("taskkill", $" /fi \"SERVICES eq {processName}\" /f");

                process?.WaitForExit();
                Logger.Info($"Process {processName} was killed.");
            }
            catch (Exception e)
            {
                Logger.Error($"Failed to kill process {processName}: {e.Message}");
            }
        }
    }
}