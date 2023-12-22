using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public Dictionary<string, float> attributes = new();
    public Dictionary<string, List<string>> formulas = new();
    public string contents;

    public void ParseFormula(List<string> lines)
    {
        lines.ForEach(x => ParseLine(x));
    }

    public void ParseLine(string line)
    {
        if (line[0] == '#')
            return;

        List<string> tokens = SplitLine(line);
        if (tokens.Count == 3) // Must be an attribute assignment
        {
            attributes.Add(tokens[0], float.Parse(tokens[2]));
            return;
        }

        // Put tokens as formula
        if (tokens.Count > 3)
        {
            List<string> subList = tokens.GetRange(2, tokens.Count - 2);
            formulas.Add(tokens[0], subList);
        }
    }

    public List<string> SplitLine(string line)
    {
        List<string> tokens = new();
        foreach (string token in line.Split(" "))
        {
            tokens.Add(token);
        }
        return tokens;
    }

}
