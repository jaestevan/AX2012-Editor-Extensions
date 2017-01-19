# Microsoft Dynamics AX 2012 X++ Editor Extensions

Initial version of this project is based on the MSDN examples of Visual Studio 2010 extensions with the idea of extending them to the Microsoft Dynamics AX 2012 X++ source code editor.

If you have any idea for improving this extensions or you discover a bug, please post to the Discussions area or Issue Tracker. We are open to suggestions!


## How to install the extensions

* [Downloads](http://ax2012editorext.codeplex.com/releases) the zip file with the libraries or clone the repository and build it yourself.
* Close your Dynamics AX 2012 client.
* **Unzip and copy** the libraries that you want to use to the AX client folder on disk 
..* By default: _C:\Program Files (x86)\Microsoft Dynamics AX\60\Client\Bin\EditorComponents_.
* You can copy all assemblies or only the ones you want to use.
* That's all!! Just open your Dynamics AX 2012 Development client and enjoy.

## Building

* If you want to build this project to make changes yourself, it need to be built with **Visual Studio 2010 SDK**.
* Download it **[here](http://www.microsoft.com/en-us/download/details.aspx?id=21835)**.

## Troubleshooting & Known issues

* Some times Extensions don't work because Windows blocks the file after downloading it from internet. 
..* [Tommy Skaue has posted in his blog a possible fix to this problem](http://yetanotherdynamicsaxblog.blogspot.com.es/2013/03/free-editor-extensions-for-ax2012.html?showComment=1363082266457#c2118124613862283410]).

# Extensions:

## Brace Matching Extension

![Brace Matching Extension](/docs/img/ax-ext-bracematching.png?raw=true "Brace Matching Extension")

## Highlight Words Extension

![Words Extension](/docs/img/ax-ext-highlightword.png?raw=true "Words Extension")

## Outlining Extension

![Outlining Extension](/docs/img/ax-ext-outlining-v2.png?raw=true "Outlining Extension")