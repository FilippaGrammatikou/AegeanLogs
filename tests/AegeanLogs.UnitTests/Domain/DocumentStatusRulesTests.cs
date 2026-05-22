using AegeanLogs.Domain.Enums;
using AegeanLogs.Domain.Rules;

namespace AegeanLogs.UnitTests.Domain;

public class DocumentStatusRulesTests
{
    [Fact]
    public void CanMoveTo_WhenRequiredToUploaded_ReturnsTrue()
    {
        var result = DocumentStatusRules.CanMoveTo(DocumentStatus.Required, DocumentStatus.Uploaded);
        Assert.True(result);
    }

    [Fact]
    public void CanMoveTo_WhenRequiredToChecked_ReturnsFlase()
    {
        var result = DocumentStatusRules.CanMoveTo(DocumentStatus.Required, DocumentStatus.Checked);
        Assert.False(result);
    }

    [Fact]
    public void CanMoveTo_WhenUploadedToChecked_ReturnTrue()
    {
        var result = DocumentStatusRules.CanMoveTo(DocumentStatus.Uploaded, DocumentStatus.Checked);
        Assert.True(result);
    }

    [Fact]
    public void CanMoveTo_WhenRejectedToUploaded_ReturnFalse()
    {
        var result = DocumentStatusRules.CanMoveTo(DocumentStatus.Rejected, DocumentStatus.Uploaded);
        Assert.False(result);
    }
}
