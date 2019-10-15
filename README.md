---
page_type: sample
languages:
- csharp
products:
- azure
description: "This is a sample HTTP trigger Azure Function that returns a ICS file that to the client."
urlFragment: ics-file-generator
---

# C# Azure Function for generating an ICS File 

This is a sample HTTP trigger Azure Function that returns a ICS file that to the client. The project takes a dependency on [iCal.NET](https://github.com/rianjs/ical.net), but demonstrates how a simple HTTP Trigger can be implemented. 

## Additional Details and History

This project originally came out a need to create an ICS file for the [Azure Functions](http://aka.ms/AzureFunctionsLive) webinar. Armed with only the knowledge of what I wanted to create, below is how I started investigating how to do this with Azure Functions : 

I began by taking a look at a third party library called [iCal.NET](https://github.com/rianjs/ical.net). It looks like this handles all the various time zones and client. Next, I opened the Azure Portal and creating a new Azure Function application securing the name that folks would go to : 
 
![image](/img/azure-new-app1.png)

Next, I chose a language and happily picked C# and used a HttpTrigger template. 

![image](/img/azure-quickstart-templates.png)

By default, the code in the template is looking for a name parameter and a POST command. Since, I didn't want to respond to a POST, but rather just a GET, I used this as my start but deleted the name parameter section. 

Armed with just the online editor, I started writing code. I could have downloaded [Visual Studio 2017 Preview 3](https://www.visualstudio.com/vs/preview/) and the [Azure Function tooling](https://blogs.msdn.microsoft.com/webdev/2017/05/10/azure-function-tools-for-visual-studio-2017/) for a richer debugging experience, but it was getting late and I wanted to crank this out with just a browser and my C# skills. 

I needed to pull in a third party library, so I noticed that you could view "Files" inside the portal and that it supports NuGet. I simply created a `project.json` and added the following NuGet package reference to iCal.NET and Newtonsoft.Json : 

```json
{
  "frameworks": {
    "net46": {
      "dependencies": {
	"Ical.Net": "2.2.19",
	"Newtonsoft.Json": "9.0.1"
      }
    }
   }
}
```

When I switched back to my function, I noticed my project started pulling down the references and restarted my function. 

The code is pretty self-explanatory if you've used iCal.NET before, but basically I create the file and ensure when someone visits the site that it sends down an ICS file named `webinarinvite.ics`. 

During the process of working with the function, I hit the "Save" button and it would let me know if it compiled successfully or not. It finally compiled successfully as shown below : 

	2017-06-06T23:19:02  Welcome, you are now connected to log-streaming service.
	2017-06-06T23:20:02  No new trace in the past 1 min(s).
	2017-06-06T23:23:04.794 Function started (Id=xxx-1d71-4f1f-8fbd-6d1b4dcc96f0)
	2017-06-06T23:23:05.921 Function completed (Success, Id=xxx-1d71-4f1f-8fbd-6d1b4dcc96f0, Duration=1137ms)

Very cool! Now to test the function, it was as simple as selecting **GET** from the dropbox on the Test Tab and hitting the **Run** button. 

![image](/img/testazurefunctions.png)

Now that my function was working properly, I grabbed the URL to my app and pasted it into the browser. The URL was easy to find as shown below:  

![image](/img/azurefunctionurl.png)

Success! It downloaded the .ICS file and after opening it, the following entry appeared in my calendar : 

![image](/img/azurewebinar.png)

I've recently checked the traffic using the "Monitor" section and noticed it has already received 393 downloads! 

![image](/img/azurestatsfunction.png)

Whenever I need to update this in the future, all it will require is changing two lines. No additional servers, redeployment, etc. is needed as it will auto compile. 

Btw, you can try it now at [http://aka.ms/AzureFunctionsLive](http://aka.ms/AzureFunctionsLive) and be sure to follow [@AzureFunctions](https://twitter.com/AzureFunctions) on Twitter! 

## Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
