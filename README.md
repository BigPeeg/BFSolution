# Lending Platform Exercise

This Console application is an implementation of a Lending Platform against the [provided requirements](https://github.com/BlackfinchGroup/Challenge).

## Overview
The application prompts the user for 'loan application details' and makes a 'decision' on the success of the application against a set of rules.

After the processing of each application, the result is published along with a report summarising the applications that have been made to date.

The user is given the opportunity to process further applications or quit from the application.

### User Input
Basic checks are carried out on the user input as an Anti-corruption Layer to ensure valid data is passed to the loan application process. The metrics are reported as simple string output to the console.

### Applying Business Rules
The rules that are to be applied to each application are hard-coded using the [Chain of Responsibility Pattern](https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern). Each rule is encapsulated with a handler with the handlers control to the next handler if it's businesss logic cannot be used to approve the application.

### Recording Metrics
Once processing of the application is complete the result are collated for the metrics report. Metric are calculated and updated as the results are received.


## Production Enhancements
### Traceability
To provide better reporting and traceability a 'transaction identity' should be applied to each application when it is submitted for approval. 

The application result is currently returned as a enum value. This should be refined to use a 'Result Object' that combines the status enumeration with any associated messaging. This would support enhanced reporting that provides an explanation in the event of an application failing. 

### Rule Configuration
The rules are currently hard-coded. Whilst the supports the addition of further rules, a code update is required for each modification. The rules could be loaded from configuration, either locally in a file, or if from some form of persistence (database, network storage, blob storage). This would enable modification to be made without code changes.

Loading the rules by configuration would support multiple instances of the application, with centralised rules being applied consistently.

### Error Handling & Reporting
There is little in the way of error handling or reporting. Production code should provide sufficient information to support investigation in the event of run-time issues.

### Analytics
Since the requirements define a simple set of metrics to be delivered, the current implementation doesn't over-engineer the solution. The reporting should be refined to use structure formatting (CSV, XML, JSON).

When moving to production it would be beneficial to persist application information, keyed by the 'transaction identity' discussed above. With an extensive possible scope of requirements around metrics using the [Commmand Query Response Segregation Pattern](https://en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation) would enable the Application Result event to be persisted with a Change Feed Stream used to then trigger any post-processing that is required to render materialised views, or forward event information into external analytics platforms.

If multiple users are processing applications simultaneously, then Identify Managment should be introduced to verify the user, and submit a user identity along with the application results.

Persisting the metrics centrally would enable multiple instances of the application discussed above, with all instances of the application pulling results from a single location. 