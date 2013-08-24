using System.Collections.Generic;

namespace MultiMonitorHelper.Common.Interfaces
{
    /// <summary>
    /// Each display model implementation is abstraced away with help of IDisplayModel.
    /// </summary>
    public interface IDisplayModel
    {
        /// <summary>
        /// Call this if you want to receive list of currently active monitors.
        /// What does "active" mean in our context? It means the monitors that are "enabled"
        /// in Desktop properties screen. 
        /// </summary>
        /// <returns>list of active monitors</returns>
        IEnumerable<IDisplay> GetActiveMonitors();

        /// <summary>
        /// Call this if you want to save current monitor configuration.
        /// You can later try and restore this specific configuration, if you need.
        /// </summary>
        /// <returns>List of monitors that needs to be passed LoadConfiguration() method.</returns>
        IList<IDisplay> SaveCurrentConfiguration();

        /// <summary>
        /// Call this if you want to load "saved" monitor configuration.
        /// This can be obtained from SaveCurrentConfiguration().
        /// </summary>
        /// <param name="monitors"></param>
        /// <returns></returns>
        bool LoadConfiguration(IList<IDisplay> monitors);
    }
}