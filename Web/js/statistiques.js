const API_BASE = "https://can.iutrs.unistra.fr/api";

async function chargerStats() {
    try {
        const resL = await fetch(`${API_BASE}/liaison/all`);
        const liaisons = await resL.json();
        
        let caGlobal = 0;
        const container = document.querySelector('[data-role="stats-liaisons"]');
        container.innerHTML = "";

        for (const l of liaisons) {
            const resC = await fetch(`${API_BASE}/liaison/${l.id}/chiffreAffaire`);
            const data = await resC.json();
            
            const totalLiaison = data.passagers.chiffreAffaire + data.vehicules.chiffreAffaire;
            caGlobal += totalLiaison;

            container.innerHTML += `
                <article class="carte-stat">
                    <header>${l.nom}</header>
                    <ul>
                        <li>CA : <strong>${totalLiaison.toFixed(2)} €</strong></li>
                        <li>Passagers : ${data.passagers.nombre}</li>
                        <li>Véhicules : ${data.vehicules.quantite}</li>
                    </ul>
                </article>`;
        }

        document.querySelector('[data-lier="caGlobal"]').textContent = `${caGlobal.toFixed(2)} €`;
    } catch (e) { console.error(e); }
}

chargerStats();