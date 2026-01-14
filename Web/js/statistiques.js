// URL de base pour l'API, elle sera forgé par la suite pour obtenir les bonnes données
const API_BASE = "https://can.iutrs.unistra.fr/api";

/**
 * Fonction asynchrone qui récupère et affiche les statistiques de chiffre d'affaires CA
 * pour l'ensemble des liaisons maritimes.
 */
async function chargerStats() {
    try {
        // Récupération de la liste complète des liaisons disponibles via l'API
        const resL = await fetch(`${API_BASE}/liaison/all`);
        const liaisons = await resL.json();
        
        // Initialisation de l'accumulateur pour le CA total de l'entreprise
        let caGlobal = 0;

        // Sélection du conteneur HTML où seront injectées les cartes de statistiques
        const container = document.querySelector('[data-role="stats-liaisons"]');
        
        // Nettoyage du conteneur pour éviter les doublons en cas de rafraîchissement
        container.innerHTML = "";

        // Parcours de chaque liaison récupérée avec une boucle for of
        for (const l of liaisons) {
            // Pour chaque liaison, on effectue un second appel API pour obtenir ses chiffres spécifiques
            // On utilise l'ID de la liaison (l.id) pour cibler la bonne ressource
            const resC = await fetch(`${API_BASE}/liaison/${l.id}/chiffreAffaire`);
            const data = await resC.json();
            
            // Calcul du CA de la liaison en sommant la partie passagers et la partie véhicules
            const totalLiaison = data.passagers.chiffreAffaire + data.vehicules.chiffreAffaire;
            
            // Ajout du résultat au montant global
            caGlobal += totalLiaison;

            //Utilisation des "Template Literals" pour insérer des variables dans le HTML
            container.innerHTML += `
                <article class="carte-stat">
                    <header>${l.nom}</header>
                    <ul>
                        <li>CA : ${totalLiaison.toFixed(2)} €</li>
                        <li>Passagers : ${data.passagers.nombre}</li>
                        <li>Véhicules : ${data.vehicules.quantite}</li>
                    </ul>
                </article>`;
        }

        // Mise à jour du total général dans le DOM après la fin de la boucle
        // .toFixed(2) permet de formater les nombres avec 2 décimales pour l'affichage monétaire (ex: 12.00€ et pas 12€ ou 12.1829€)
        document.querySelector('[data-lier="caGlobal"]').textContent = `${caGlobal.toFixed(2)} €`;

    } catch (e) { 
        // Gestion des erreurs (ex: problème réseau ou erreur de l'API)
        console.error("Erreur lors de la récupération des statistiques :", e); 
    }
}

// Lancement de la fonction au chargement du script
chargerStats();