;Inno Setup file. See http://www.jrsoftware.org/isinfo.php if you're unfamiliar with this type of file.

[Setup]
AppPublisher=KeenSystems
AppPublisherURL=http://www.keensystems.eu
AppVersion=0.4.3.0
AppName=Fop2DD
AppVerName=Fop2DD 0.4.3
AppCopyright=Copyright (C) 2013 KeenSystems
VersionInfoVersion=0.4.3.0
DefaultDirName={pf}\KeenSystems\Fop2DD
DefaultGroupName=KeenSystems\Fop2DD
UninstallDisplayIcon={app}\Fop2DD.exe
Compression=lzma2/max
SolidCompression=yes
OutputDir=..\
OutputBaseFilename=fop2dd_setup
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64
ArchitecturesAllowed=x86 x64
MinVersion=0.0,5.1
UsePreviousLanguage=yes
DisableWelcomePage=yes
SetupIconFile=icons\application.ico
AllowUNCPath=no
;AppMutex must be in lowercase!
AppMutex=9F94F10A-CAEB-4DC5-B1F1-C6001C1B4D91

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "nl"; MessagesFile: "compiler:Languages\Dutch.isl"

[Types]
Name: "full";   Description: {cm:FullInstallation}
Name: "custom"; Description: {cm:CustomInstallation}; Flags: iscustom

[Components]
Name: "main";           Description: {cm:MainFiles};           Types: full custom; Flags: fixed
Name: "language\dutch"; Description: {cm:LanguageFiles_Dutch}; Types: full

[Files]
Source: "bin\release\Fop2DD.exe";           DestDir: "{app}";    Components: main;           Flags: replacesameversion
Source: "bin\release\Fop2ClientLib.dll";    DestDir: "{app}";    Components: main;           Flags: replacesameversion
Source: "bin\release\GlobalHotKey.dll";     DestDir: "{app}";    Components: main;           Flags: replacesameversion
Source: "bin\release\Newtonsoft.Json.dll";  DestDir: "{app}";    Components: main;           Flags: replacesameversion
Source: "bin\release\NLog.dll";             DestDir: "{app}";    Components: main;           Flags: replacesameversion
Source: "bin\release\NLog.config";          DestDir: "{app}";    Components: main;           Flags: onlyifdoesntexist

[Icons]
Name: "{group}\Fop2DD";         Filename: "{app}\Fop2DD.exe"
Name: "{group}\{cm:UnInstall}"; Filename: "{uninstallexe}"

[Tasks]
Name: launchonboot;   Description: {cm:LaunchOnBoot};   
Name: calltohandler;  Description: {cm:CalltoHandler};  
Name: telhandler;     Description: {cm:TelHandler};    Flags: unchecked;

[CustomMessages]
en.FullInstallation=Full installation
en.CustomInstallation=Custom installation
en.MainFiles=Fop2DD program files (required)
en.LanguageFiles_Dutch=Dutch language files
en.LaunchOnBoot=Set Fop2DD to launch when Windows boots
en.CalltoHandler=Set Fop2DD to handle callto: links
en.TelHandler=Set Fop2DD to handle tel: links
en.UnInstall=Uninstall Fop2DD

nl.FullInstallation=Volledige installatie
nl.CustomInstallation=Aangepaste installatie
nl.MainFiles=Fop2DD programma bestanden (vereist)
nl.LanguageFiles_Dutch=Nederlandse taalbestanden
nl.LaunchOnBoot=Stel Fop2DD in om te starten wanneer Windows opstart
nl.CalltoHandler=Stel Fop2DD in om callto: links af te handelen
nl.TelHandler=Stel Fop2DD in om tel: links af te handelen
nl.UnInstall=Fop2DD Verwijderen

[Registry]
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "Fop2DD";       ValueData: "{app}\Fop2DD.exe";             Flags: uninsdeletevalue; Tasks: launchonboot;

Root: HKCR; Subkey: "callto";                                        ValueType: string; ValueName: "";             ValueData: "URL:Callto Protocol";          Flags: uninsdeletevalue; Tasks: CalltoHandler;
Root: HKCR; Subkey: "callto";                                        ValueType: string; ValueName: "URL Protocol"; ValueData: "";                             Flags: uninsdeletevalue; Tasks: CalltoHandler;
Root: HKCR; Subkey: "callto\DefaultIcon";                            ValueType: string; ValueName: "";             ValueData: "{app}\Fop2DD.exe,1";           Flags: uninsdeletevalue; Tasks: CalltoHandler;
Root: HKCR; Subkey: "callto\shell\open\command";                     ValueType: string; ValueName: "";             ValueData: """{app}\Fop2DD.exe"" ""%1""";  Flags: uninsdeletevalue; Tasks: CalltoHandler;

Root: HKCR; Subkey: "tel";                                           ValueType: string; ValueName: "";             ValueData: "URL:Callto Protocol";          Flags: uninsdeletevalue; Tasks: TelHandler;
Root: HKCR; Subkey: "tel";                                           ValueType: string; ValueName: "URL Protocol"; ValueData: "";                             Flags: uninsdeletevalue; Tasks: TelHandler;
Root: HKCR; Subkey: "tel\DefaultIcon";                               ValueType: string; ValueName: "";             ValueData: "{app}\Fop2DD.exe,1";           Flags: uninsdeletevalue; Tasks: TelHandler;
Root: HKCR; Subkey: "tel\shell\open\command";                        ValueType: string; ValueName: "";             ValueData: """{app}\Fop2DD.exe"" ""%1""";  Flags: uninsdeletevalue; Tasks: TelHandler;

[Run]
Filename: "{app}\Fop2DD.exe"; Parameters: "{src}\setup.json"; Flags: postinstall nowait;