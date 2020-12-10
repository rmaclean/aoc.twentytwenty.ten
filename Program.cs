using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = (await File.ReadAllLinesAsync("data.txt"))
    .Select(line => Convert.ToInt32(line))
    .OrderBy(jolt => jolt)
    .ToArray();


var knownConnections = new Dictionary<int, long>();

// Part1();
Part2();

long CountLeafs(Node node)
{
    var known = 0L;
    if (knownConnections.TryGetValue(node.Jolt, out known))
    {
        return known;
    }

    if (node.ValidConnections.Count == 0)
    {
        knownConnections.Add(node.Jolt, 1L);
        return 1;
    }

    var result = 0L;
    foreach (var child in node.ValidConnections)
    {
        result += CountLeafs(child);
    }

    knownConnections.Add(node.Jolt, result);
    return result;
}

void Part2()
{
    var nodes = new List<Node>();

    for (var index = 0; index < lines.Length; index++)
    {
        var adapter = new Node(lines[index]);
        var validNodes = nodes.Where(n => adapter.Jolt - n.Jolt <= 3);
        foreach (var validNode in validNodes)
        {
            validNode.ValidConnections.Add(adapter);
        }

        nodes.Add(adapter);
    }
    
    var paths = nodes.Where(n => n.Jolt <= 3).Select(n => CountLeafs(n)).Sum();
        
    Console.WriteLine($"Paths {paths}");
}

void Part1()
{
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
}

public record Node(int Jolt, List<Node> ValidConnections)
{
    public Node(int Jolt):this(Jolt, new List<Node>()) {}
}