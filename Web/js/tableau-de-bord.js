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
            let couleurClass = "";
            let txtClass = "";

            if (txV <= 50) { // si taux véhicules <= 50% alors..
                couleurClass = "couleur-ok";    // Vert
                txtClass = "txt-ok";
            } else if (txV <= 75) {
                couleurClass = "couleur-avert"; // Orange
                txtClass = "txt-avert";
            } else if (txV <= 100) {
                couleurClass = "couleur-alerte"; // Rouge
                txtClass = "txt-alerte";
            } else {
                couleurClass = "couleur-neon";   // Jaune fluo si on dépasse 100%
                txtClass = "txt-neon";
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
                                <span>${txP}%</span>
                            </div>
                            <div class="jauge-fond">
                                <div class="jauge-remplissage" style="background:var(--accent); width:${txP}%"></div>
                            </div>
                        </div>
                        
                        <div class="barre">
                            <div style="display:flex; justify-content:space-between">
                                <span>Véhicules</span>
                                <span class="${txtClass}">${txV}%</span>
                            </div>
                            <div class="jauge-fond">
                                <div class="jauge-remplissage ${couleurClass}" style="width:${txV}%"></div>
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