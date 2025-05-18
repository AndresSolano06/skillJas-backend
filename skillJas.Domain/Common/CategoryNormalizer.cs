namespace skillJas.Domain.Common;

public static class CategoryNormalizer
{
    private static readonly Dictionary<string, string> _map = new(StringComparer.OrdinalIgnoreCase)
    {
        // Lenguajes
        ["js"] = "JavaScript",
        ["javascript"] = "JavaScript",
        ["java_script"] = "JavaScript",
        ["ts"] = "TypeScript",
        ["typescript"] = "TypeScript",
        ["py"] = "Python",
        ["python3"] = "Python",
        ["csharp"] = "CSHARP",
        ["c#"] = "CSHARP",
        ["cs"] = "CSHARP",
        ["node"] = "Node.js",
        ["nodejs"] = "Node.js",
        ["html"] = "HTML",
        ["html5"] = "HTML",
        ["css"] = "CSS",
        ["css3"] = "CSS",
        ["sql"] = "SQL",
        ["pgsql"] = "PostgreSQL",
        ["postgres"] = "PostgreSQL",

        // Frameworks
        ["react"] = "React",
        ["angular"] = "Angular",
        ["vue"] = "Vue",
        ["dotnet"] = ".NET",
        [".net"] = ".NET",
        ["asp.net"] = "ASP.NET",
        ["bpmn"] = "BPMN",

        // Herramientas / IDEs
        ["vscode"] = "VS Code",
        ["visualstudio"] = "Visual Studio",
        ["jira"] = "Jira",
        ["git"] = "Git",
        ["github"] = "GitHub",
        ["figma"] = "Figma",

        // Roles
        ["frontend"] = "Frontend",
        ["front"] = "Frontend",
        ["back"] = "Backend",
        ["backend"] = "Backend",
        ["fullstack"] = "Fullstack",
        ["qa"] = "QA",
        ["dev"] = "Developer",
        ["developer"] = "Developer"
    };

    public static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "Otro";

        var key = input.Trim().ToLowerInvariant();
        return _map.TryGetValue(key, out var normalized)
            ? normalized
            : char.ToUpper(key[0]) + key[1..];
    }
}
