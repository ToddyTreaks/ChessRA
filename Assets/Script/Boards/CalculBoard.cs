using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculBoard : MonoBehaviour
{
    public static CalculBoard _instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    /*
     * Attributs : tableau de enum de type de pièce & team
     *
     * Initialise le tableau d'enum
     * Fonction MoveVerification qui regarde si une liste de positions est possible ( légalité des positions ), cas de l'échec et cas du mat
     *      renvoie une liste de positions légaux
     *
     */
}