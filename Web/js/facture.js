const tarifsPassagers = {
    'adu26p': { 'groix': 18.75, 'belle-ile': 18.80, 'libelle': 'Adulte de 26 ans et plus' },
    'jeu1825': { 'groix': 13.80, 'belle-ile': 14.10, 'libelle': 'Jeune de 18 à 25 ans inclus' },
    'enf417': { 'groix': 11.25, 'belle-ile': 11.65, 'libelle': 'Enfant de 4 à 17 ans inclus' },
    'bebe': { 'groix': 0, 'belle-ile': 0, 'libelle': 'Bébé de moins de 4 ans' },
    'ancomp': { 'groix': 3.35, 'belle-ile': 3.35, 'libelle': 'Animal de compagnie' }
};

const tarifsVehicules = {
    'trot': { 'groix': 4.70, 'belle-ile': 4.70, 'libelle': 'Trottinette électrique' },
    'velo': { 'groix': 8.20, 'belle-ile': 8.20, 'libelle': 'Vélo ou remorque à vélo' },
    'velelec': { 'groix': 11.00, 'belle-ile': 11.00, 'libelle': 'Vélo électrique' },
    'cartand': { 'groix': 16.45, 'belle-ile': 16.45, 'libelle': 'Vélo cargo ou tandem' },
    'mobil': { 'groix': 23.10, 'belle-ile': 23.35, 'libelle': 'Deux-roues <= 125 cm3' }, // dire qu'une 125cc3 c'est une mobilette c'est insultant 
    'moto': { 'groix': 66.05, 'belle-ile': 66.40, 'libelle': 'Deux-roues > 125 cm3' },
    'cat1': { 'groix': 96.05, 'belle-ile': 98.50, 'libelle': 'Voiture de moins de 4m' },
    'cat2': { 'groix': 114.80, 'belle-ile': 117.20, 'libelle': 'Voiture de 4m à 4,39m' },
    'cat3': { 'groix': 174.45, 'belle-ile': 176.90, 'libelle': 'Voiture de 4,40m à 4,79m' },
    'cat4': { 'groix': 210.90, 'belle-ile': 213.35, 'libelle': 'Voiture de 4,80 m et plus' },
    'camp': { 'groix': 330.20, 'belle-ile': 332.70, 'libelle': 'Camping-car / véhicule de plus de 2,10m de haut' }
};