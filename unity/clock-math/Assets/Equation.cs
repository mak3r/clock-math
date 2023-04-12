using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class Equation : MonoBehaviour
{
    public List<string> eqValues = new List<string>{"","","", "","", "",""};
    private DataTable table = new DataTable();

    public bool Evaluate() {
        DumpValues();
        if (string.IsNullOrEmpty(eqValues[3]) || string.IsNullOrEmpty(eqValues[5])) {
            // Debug.Log("Invalid equation: ");
            return false;
        }
        string equation = "";
        if (!string.IsNullOrEmpty(eqValues[0])) {
            if (!string.IsNullOrEmpty(eqValues[1])) {
                equation = equation + eqValues[0] + " " + eqValues[1] + " ";
            } else {
                // Debug.Log("Invalid equation: " + equation);
                return false;
            }
        } 
        //insert remaining fields
        equation = equation +
        eqValues[2] + " " +
        eqValues[3] + " " +
        eqValues[4] + " " +
        eqValues[5] + " " +
        eqValues[6];
        Debug.Log("Equation: " + equation);
        object result = table.Compute(equation,null);
        if (result is bool) {
            return (bool)result;
        }
        return false;
    }

    private void DumpValues() {
        int i = 0;
        foreach (string s in eqValues) {
            Debug.Log("eqValues[" + i + "]: " + s );
            i++;
        }
    }

}

