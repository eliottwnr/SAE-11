using System; 

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        uint[] date = saisirDate();
        string[] horaires;

        Liaison liaison = saisirLiaison();
        
        horairesJour(liaison, date[0], out horaires); // date[0] correspond au jour
        afficherHoraires(horaires); 
    }
}
