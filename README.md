# NAShutdown
Easy console app that allows to shutdown poorly secured [D-Link DNS-320](https://www.google.cz/search?q=D-Link+DNS-320).

## About
While ago I bought NAS - D-Link DNS-320, it worked fine but I didn't like one thing... to shut it down you must eather push the button on NAS for few seconds or open the administration in webbrowser and find appropriate setting.

So I thought, there must be better way. And yes there was a better way :relaxed:
I examined requests that are sent from web administration and coppied the one that sends shutdown command. Then I wrote simple console app that sends same request. It was way easier than I thought, becase as "authentication" they use cookie `username:admin` :satisfied:

Now everything I need to shutdown the NAS is to run this app :thumbsup:
