﻿namespace TSWMS.UserService.Shared.Options;

public class EncryptionOptions
{
    public string Key { get; set; } = string.Empty;
    public string IV { get; set; } = string.Empty;
}
