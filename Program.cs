using System;
using System.IO;
using System.Linq;

var lines = (await File.ReadAllLinesAsync("data.txt"))
    .Select(line => Convert.ToInt32(line))
    .OrderBy(jolt => jolt);

var currentJolt = 0;
var oneJoltJumps = 0;
var threeJoltJumps = 0;

foreach (var line in lines)
{
    var diff = line - currentJolt;
    currentJolt = line;
    if (diff > 3)
    {
        throw new Exception("everything broken");
    }

    switch (diff)
    {
        case 1: 
        {
            oneJoltJumps++;
            break;
        }
        case 3: 
        {
            threeJoltJumps++;
            break;
        }
    }
}

threeJoltJumps++; // since we always 3 more

Console.WriteLine($"One jumps {oneJoltJumps}");
Console.WriteLine($"Three jumps {threeJoltJumps}");
Console.WriteLine($"Answer is {oneJoltJumps * threeJoltJumps}");