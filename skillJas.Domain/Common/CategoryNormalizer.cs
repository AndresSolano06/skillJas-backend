namespace skillJas.Domain.Common;

public static class CategoryNormalizer
{
    public static readonly Dictionary<string, string> Map = new(StringComparer.OrdinalIgnoreCase)
    {
        // Lenguajes
        { "js", "JavaScript" }, { "javascript", "JavaScript" }, { "java script", "JavaScript" },
        { "ts", "TypeScript" }, { "typescript", "TypeScript" },
        { "py", "Python" }, { "python3", "Python" },
        { "c#", "C#" }, { "csharp", "C#" },
        { "c++", "C++" }, { "cpp", "C++" },
        { "java", "Java" },
        { "go", "Go" }, { "golang", "Go" },
        { "rust", "Rust" },
        { "php", "PHP" },
        { "sql", "SQL" },
        { "kotlin", "Kotlin" },
        { "swift", "Swift" },
        { "bash", "Bash" },
        { "r", "R" },
        { "dart", "Dart" },

        // Frameworks
        { "react", "React" },
        { "vue", "Vue" },
        { "angular", "Angular" },
        { "nextjs", "Next.js" }, { "next.js", "Next.js" },
        { "nestjs", "NestJS" },
        { "spring", "Spring" },
        { "django", "Django" },
        { "flask", "Flask" },
        { "laravel", "Laravel" },
        { "express", "Express" },

        // IDE / Herramientas
        { "vs code", "VS Code" },
        { "visual studio", "Visual Studio" },
        { "intellij", "IntelliJ" },
        { "pycharm", "PyCharm" },
        { "webstorm", "WebStorm" },

        // Roles / Cargos
        { "frontend", "Frontend" },
        { "backend", "Backend" },
        { "fullstack", "Fullstack" },
        { "devops", "DevOps" },
        { "qa", "QA" },
        { "scrum master", "Scrum Master" },
        { "tech lead", "Tech Lead" }
    };

    public static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var key = input.Trim().ToLower();
        return Map.TryGetValue(key, out var result)
            ? result
            : char.ToUpper(key[0]) + key[1..];
    }
}
