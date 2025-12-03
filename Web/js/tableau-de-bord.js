const capacitesBateaux = {
    'Breizh Nevez': { passagers: 450, vehicules: 60 },  
    'Vindilis': { passagers: 600, vehicules: 85 }       
};

// Horaire des trajets
const horaires = {
    1: ['07:00', '09:45', '11:00', '14:30', '17:30'],  // pour Lorient vers Groix
    2: ['08:15', '10:45', '13:00', '15:45', '18:30'],  // pour Groix vers Lorient
    3: ['07:30', '09:15', '11:45', '14:00', '17:00'],  // pour Quiberon vers Le Palais
    4: ['08:45', '10:30', '13:15', '15:30', '18:15']   // pour Le Palais vers Quiberon
};