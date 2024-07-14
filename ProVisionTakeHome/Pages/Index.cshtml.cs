using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//added namespaces for CliWrap
using System.Diagnostics;
using CliWrap;
using CliWrap.Buffered;

namespace ProVisionTakeHome.Pages;

public class IndexModel : PageModel
{
    //public global variable for output
    public required List<String> OutputList { get; set; }

    //Method that uses CliWrap to get the versions of 4 installed programs and writes results to global OutputList variable
    public async Task OnGet()
    {
        //List of commands used to get program versions
        List<List<String>> commandList = new List<List<String>>();
        commandList.Add(["python", "-V"]);
        commandList.Add(["npm", "--version"]);
        commandList.Add(["dotnet", "--version"]);
        commandList.Add(["node", "--version"]);

        //for loop that uses CliWrap to run commands and adds to global output list
        OutputList = new List<String>();
        foreach (List<String> command in commandList) {
            var result = await Cli.Wrap(command[0])
                .WithArguments(command[1])
                .ExecuteBufferedAsync();
            OutputList.Add(command[0] + ": " + result.StandardOutput);
        }
    }
}