// URL de base pour l'API, elle sera forgé par la suite pour obtenir les bonnes données
const API_BASE = "https://can.iutrs.unistra.fr/api";

// fait correspondre les valeurs textuelles (lorient-groix, groix-lorient...) aux ID numériques (1, 2, 3, 4)
const LIAISONS_MAP = {
    "lorient-groix": 1,
    "groix-lorient": 2,
    "quiberon-lepalais": 3,
    "lepalais-quiberon": 4
};

// Gestion de la soumission du formulaire de recherche de remplissage du bateau
document.querySelector('form').onsubmit = async (e) => {
    // Empêche le rechargement par défaut de la page lors de la soumission du formulaire
    e.preventDefault();

    // Utilisation de l'objet FormData pour extraire  les valeurs des champs du formulaire (obligatoire)
    const formData = new FormData(e.target);
    const liaisonKey = formData.get('liaison'); // Récupère la valeur de l'attribut 'name="liaison"'
    const date = formData.get('date');          // Récupère la date sélectionnée
    
    // Récupération de l'ID correspondant via le dictionnaire LIAISONS_MAP
    const idLiaison = LIAISONS_MAP[liaisonKey];

    // Appel à l'API pour récupérer les données de remplissage pour la liaison et la date données
    const res = await fetch(`${API_BASE}/liaison/${idLiaison}/remplissage/${date}`);
    const donnees = await res.json();

    // Sélection du conteneur où seront affichées les cartes de traversées
    const container = document.querySelector('[data-role="traversees"]');
    
    // Réinitialisation du contenu pour effacer les résultats d'une recherche précédente, éviter les doublons
    container.innerHTML = "";

    // Parcours du tableau de résultats renvoyé par l'API
    donnees.forEach(t => {
        // Calcul du taux d'occupation en pourcentage pour les passagers et les véhicules
        // Formule : (Nombre réservé / Capacité totale) * 100
        // Math.round() pour arrondir à l'entier le plus proche
        const txPassagers = Math.round((t.nbReservationPassagers / t.capacitePassagers) * 100);
        const txVehicules = Math.round((t.nbReservationVoitures / t.capaciteVoitures) * 100);

        container.innerHTML += `
            <article class="carte-tableau">
                <header>${t.heure}</header>
                <div class="barres">
                    <div class="barre">
                        <span>Passagers (${txPassagers}%)</span>
                        <div style="background: lightblue; width: ${txPassagers}%">&nbsp;</div>
                    </div>
                    <div class="barre">
                        <span>Véhicules (${txVehicules}%)</span>
                        <div style="background: orange; width: ${txVehicules}%">&nbsp;</div>
                    </div>
                </div>
            </article>`;
    });
};