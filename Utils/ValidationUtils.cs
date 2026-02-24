using System;
using System.Text.RegularExpressions;

namespace CFDIEstatusMXBSv.Utils;

public static class ValidationUtils
{
    public static bool IsValidRfc(string rfc)
    {
        if (string.IsNullOrWhiteSpace(rfc)) return false;
        var v = rfc.Trim().ToUpperInvariant();
        var pattern = "^([A-ZÑ&]{3}|[A-ZÑ&]{4})\\d{6}[A-Z0-9]{3}$";
        return Regex.IsMatch(v, pattern);
    }

    public static bool IsValidUuidOrFolio(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return false;
        var v = id.Trim();
        if (Guid.TryParse(v, out _)) return true;
        return v.Length == 36 || v.Length == 32;
    }

    public static bool IsValidFE(string fe)
    {
        if (string.IsNullOrWhiteSpace(fe)) return false;
        var v = fe.Trim();
        if (v.Length == 8) return true;
        return false;
    }
}
