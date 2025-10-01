
namespace lan_scanner.core
{
    public class AppCore
    {
        private static readonly AppCore _instance = new AppCore();
        public static AppCore Instance => _instance;

        // =====================================================================================================
        public string ProjectDir { get; private set; }
        public string SourcesDir { get; private set; }
        public string ResourcesDir { get; private set; }
        public string ImgResourcesDir
        {
            get => Path.Combine(ResourcesDir, "Images");
        }
        public string JsonResourcesDir
        {
            get => Path.Combine(ResourcesDir, "Json");
        }

        // =====================================================================================================
        private AppCore() { }
        // =====================================================================================================
        public void Initialize()
        {
            ProjectDir = GetProjectDirectory();
            ResourcesDir = Path.Combine(ProjectDir, "Resources");
            SourcesDir = Path.Combine(ProjectDir, "Sources");
        }
        // =====================================================================================================
        private string GetProjectDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            return projectDirectory;
        }
        // =====================================================================================================
    }
}
