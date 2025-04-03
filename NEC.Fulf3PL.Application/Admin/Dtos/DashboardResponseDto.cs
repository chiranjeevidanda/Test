namespace NEC.Fulf3PL.Application.Admin.Dtos;

public class DashboardResponseDto
{
    public DashboardResponseItemDto All { get; set; } = new();

    public DashboardResponseItemDto Expense { get; set; } = new();

    public DashboardResponseItemDto Invoice { get; set; } = new();

    public DashboardResponseItemDto CostCenter { get; set; } = new();

    public DashboardResponseItemDto InternalOrder { get; set; } = new();

    public DashboardResponseItemDto Employee { get; set; } = new();

    public DashboardResponseItemDto Vendor { get; set; } = new();
}

public class DashboardResponseItemDto
{
    public IEnumerable<DashboardResponseItemLabelDto> Labels { get; set; }
}

public class DashboardResponseItemLabelDto
{
    public string Label { get; set; } = string.Empty;

    public int Value { get; set; }
}
