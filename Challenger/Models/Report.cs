namespace Challenger.Models;

public class ReportParameter
{
    public string UserName { get; set; }
    public string AvatarUrl { get; set; }
    public Report Report { get; set; }
}

public class Report
{
    public int Id { get; set; }
    public int ProgrammingChallengeId { get; set; }
    public string UserId { get; set; }
    public int Points { get; set; }
    public double Duration { get; set; }
    public List<TestResult> TestResults { get; set; } = new();
}

public class TestResult
{
    public int ProgrammingChallengeReportId { get; set; }
    public string Name { get; set; }
    public double? Duration { get; set; }
    public int TotalRuns { get; set; }
    public int TotalFails { get; set; }
    public string? IncomingValues { get; set; }
    public string? AssertionMessage { get; set; }
    public TestStatus Result { get; set; }

    public List<TestResultRow> GetErrors()
    {
        if (string.IsNullOrEmpty(IncomingValues) || string.IsNullOrEmpty(AssertionMessage))
            return new();

        List<TestResultRow> results = new();
        var parameters = IncomingValues.Split("|||");
        var errors = AssertionMessage.Split("|||");

        for (int i = 0; i < parameters.Length; i++)
            results.Add(new()
            {
                Errors = errors[i],
                Parameters = parameters[i]
            });
        
        return results;
    }
}

public class TestResultRow
{
    public string Errors { get; set; }
    public string Parameters { get; set; }
}

public enum TestStatus
{
    Pass,
    Fail
}