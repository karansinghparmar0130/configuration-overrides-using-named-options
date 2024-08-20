Setup showcases how we can use named options to create environment/ context specific version of the configuration and split the configuration file in smalled chunks.

* appsettings.json acts as the default configuration file
* <>.overrides.json acts as the override file which is used to create named versions of the configuration, inheriting default values from appsettings.json

During runtime, context specific version of setting can be retrieved using named value created or get default.

Please note that I am using IOptionsMonitor (singleton) to achieve this. We can even use IOptionsSnapshot (Scoped) as well.
