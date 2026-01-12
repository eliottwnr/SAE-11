const API_BASE = "https://can.iutrs.unistra.fr/api";

const LIAISONS_MAP = {
    "lorient-groix": 1,
    "groix-lorient": 2,
    "quiberon-lepalais": 3,
    "lepalais-quiberon": 4
};

document.querySelector('form').onsubmit = async (e) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    const liaisonKey = formData.get('liaison');
    const date = formData.get('date');
    const idLiaison = LIAISONS_MAP[liaisonKey];

    const res = await fetch(`${API_BASE}/liaison/${idLiaison}/remplissage/${date}`);
    const donnees = await res.json();

    const container = document.querySelector('[data-role="traversees"]');
    container.innerHTML = "";

    donnees.forEach(t => {
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