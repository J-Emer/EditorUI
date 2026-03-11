namespace EditorUI.Util.Helpers
{
    /// <summary>
    /// Base class representing a UI "desktop" or screen.
    /// 
    /// Implementations are responsible for creating and configuring
    /// their UI elements during <see cref="Load"/>. When the desktop
    /// is closed or replaced, <see cref="Unload"/> is called to allow
    /// cleanup of any custom resources.
    /// </summary>
    public abstract class Desktop
    {
        /// <summary>
        /// Called when the desktop is loaded.
        /// 
        /// Create and add all UI elements here.
        /// This method must be implemented by derived classes.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Called when the desktop is unloaded.
        /// 
        /// Use this to release any resources created by the desktop.
        /// UI elements themselves are cleared automatically.
        /// </summary>
        public virtual void Unload() { }
    }
}