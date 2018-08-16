using System.Runtime.Serialization;

[System.Serializable]
public class AATKitConfiguration
{
    public enum Consent
    {
        OBTAINED,
        UNKNOWN,
        WITHHELD
    }

    public string AlternativeBundleId = string.Empty;

    public bool ConsentRequired = true;

    public string InitialRules = string.Empty;

    public bool ShouldCacheRules = true;

    public bool ShouldReportUsingAlternativeBundleId = true;

    public int TestModeAccountId = 0;

    public bool UseDebugShake = true;

    public Consent SimpleConsent = Consent.UNKNOWN;

	public bool ConsentAutomatic = false;

	public string ConsentString = string.Empty;
}