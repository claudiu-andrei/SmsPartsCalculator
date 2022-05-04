# SmsPartsCalculator
Small app for splitting sms text into sms parts (and getting the number of sms parts required for a given text).

### Starting locally - application
1. Run Visual Studio 2019 using SMSParts.Web as starter project and the SMSParts profile (port 5000)
2. Browse to http://localhost:5000 and see the swagger documentation.

### Running integration tests
1. SMSParts application must be started locally (default port 5000).
2. Ensure that in the appsettings file the correct localhost port is filled.
3. Run tests from Test Explorer. They will run against the given localhost environment.
