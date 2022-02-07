namespace MySvc.Framework.IS4.Domain
{
    /// <summary>Token usage types.</summary>
    public enum TokenUsage
    {
        ReUse,
        OneTimeOnly,
    }

    public enum TokenExpiration
    {
        Sliding,
        Absolute,
    }

    public enum RegisterFrom
    {
        Online,
        Invite,
        Import,
    }

    public enum ActivationStatus
    {
        Inactive,
        Activated
    }
}
