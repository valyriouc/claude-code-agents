namespace Setup;

class Program
{
    private static string AgentPath =
        Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!,
            "agents");
    
    static void Main(string[] args)
    {
        
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Expected path to the project where the agents should be placed in!");
            return;
        }

        string path = args[0];
        if (!Directory.Exists(path))
        {
            Console.Error.WriteLine("The specified project does not exist!");
            return;
        }

        if (!Directory.Exists(AgentPath))
        {
            Console.Error.WriteLine(
                "There is no agent directory! This is where the markdown descriptions of the agents are sitting in!");
            return;
        }

        IEnumerable<string> agents = Directory.EnumerateFiles(AgentPath, "*.md", SearchOption.AllDirectories);

        string claudeAgentPath = Path.Combine(
            path,
            ".claude",
            "agents");
        
        if (!Directory.Exists(claudeAgentPath))
        {
            Console.WriteLine("Creating agent path in the target directory!");
            Directory.CreateDirectory(claudeAgentPath);
        }

        foreach (var agent in agents)
        {
            string fileName = Path.GetFileName(agent);
            File.Copy(agent, Path.Combine(claudeAgentPath, fileName));
        }
    }
}