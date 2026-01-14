// URL de base pour l'API, elle sera forgé par la suite pour obtenir les bonnes données
const API_BASE = "https://can.iutrs.unistra.fr/api";

// Dictionnaire qui fait le pont entre les textes du menu déroulant et les ID que l'API attend.
const LIAISONS_MAP = {
    "lorient-groix": 1,
    "groix-lorient": 2,
    "quiberon-lepalais": 3,
    "lepalais-quiberon": 4
};

// Dès que le formulaire est envoyé, la fonction asynchrone se lance
document.querySelector('form').onsubmit = async (e) => {
    // On bloque le rechargement auto de la page sinon le JS ne peut pas faire son boulot (donc le tableau de bord n'apparaît pas)
    e.preventDefault(); // Solution de l'IA car j'ai bloqué sur ce problème

    // FormData récupère d'un coup toutes les saisies du formulaire (plus propre que de les prendre une par une)
    const formData = new FormData(e.target);
    const liaisonKey = formData.get('liaison'); // Récupère la liaison choisie
    const date = formData.get('date');          // Récupère la date
    
    // On va chercher l'ID correspondant dans notre dictionnaire (LIAISONS_MAP)
    const idLiaison = LIAISONS_MAP[liaisonKey];

    try {
        //  forge l'URL et on attend (await) que l'API réponde
        const res = await fetch(`${API_BASE}/liaison/${idLiaison}/remplissage/${date}`);
        const donnees = await res.json(); // On transforme le résultat brut en objet JS

        // On cible la zone où on va afficher les résultats
        const container = document.querySelector('[data-role="traversees"]');
        // On vide le conteneur pour ne pas cumuler avec une recherche précédente
        container.innerHTML = ""; 

        // On boucle sur chaque traversée t renvoyée par l'API
        donnees.forEach(t => {
            // Calcul des pourcentages de remplissage (Nombre / Capacité x 100)
            const txP = Math.round((t.nbReservationPassagers / t.capacitePassagers) * 100); // taux passagers
            const txV = Math.round((t.nbReservationVoitures / t.capaciteVoitures) * 100); // taux véhicules

            // Logique pour les couleurs demandées dans l'énoncé (selon le remplissage véhicules)
            let couleurClassV = "";
            let txtClassV = "";

            if (txV <= 50) { // si taux véhicules <= 50% alors..
                couleurClassV = "couleur-ok";    // Vert
                txtClassV = "txt-ok";
            } else if (txV <= 75) {
                couleurClassV = "couleur-avert"; // Orange
                txtClassV = "txt-avert";
            } else if (txV <= 100) {
                couleurClassV = "couleur-alerte"; // Rouge
                txtClassV = "txt-alerte";
            } else {
                couleurClassV = "couleur-neon";   // Jaune fluo si on dépasse 100%
                txtClassV = "txt-neon";
            }

            // Logique de couleur identique appliquée aux passagers
            let couleurClassP = "";
            let txtClassP = "";

            if (txP <= 50) {
                couleurClassP = "couleur-ok";
                txtClassP = "txt-ok";
            } else if (txP <= 75) {
                couleurClassP = "couleur-avert";
                txtClassP = "txt-avert";
            } else if (txP <= 100) {
                couleurClassP = "couleur-alerte";
                txtClassP = "txt-alerte";
            } else {
                couleurClassP = "couleur-neon";
                txtClassP = "txt-neon";
            }

            // Injection du HTML pour chaque carte
            // On utilise les `` pour pouvoir écrire sur plusieurs lignes
            // et ${} pour insérer les variables 
            container.innerHTML += `
                <article class="carte-tableau">
                    <header><strong>${t.heure}</strong></header>
                    <div class="barres">
                        
                        <div class="barre">
                            <div style="display:flex; justify-content:space-between">
                                <span>Passagers</span>
                                <span class="${txtClassP}">${txP}%</span>
                            </div>
                            <div class="jauge-fond">
                                <div class="jauge-remplissage ${couleurClassP}" style="width:${txP}%"></div>
                            </div>
                        </div>
                        
                        <div class="barre">
                            <div style="display:flex; justify-content:space-between">
                                <span>Véhicules</span>
                                <span class="${txtClassV}">${txV}%</span>
                            </div>
                            <div class="jauge-fond">
                                <div class="jauge-remplissage ${couleurClassV}" style="width:${txV}%"></div>
                            </div>
                        </div>

                    </div>
                </article>`;
        });
    } catch (e) {
        //  log en console si le fetch échoue (problème réseau ou API)
        console.error("Erreur lors du chargement :", e);
    }
};
