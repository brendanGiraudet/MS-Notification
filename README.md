# Ce que fait ce `README.md` :

- **Caractéristiques** : Décrit ce que fait le microservice `ms-notification`.
- **Prérequis** : Énumère les outils nécessaires pour construire et exécuter le service.
- **Installation** : Fournit des instructions pour cloner le dépôt et accéder au répertoire.
- **Configuration** : Explique que le microservice utilise une base de données.
- **Construire et Exécuter le Microservice** : Explique comment construire et exécuter le service localement et via Docker.
- **Utilisation** : Explique comment utiliser les points de terminaison OData pour interagir avec le service.
- **Exemples de Requêtes avec Postman** : Fournit des exemples concrets de requêtes pour tester le service.
- **Contribuer** : Encouragement à contribuer au projet.
- **License** : Décrit la licence sous laquelle le projet est distribué.

## Caractéristiques
`ms-notification` est un microservice basé sur .NET Minimal APIs et OData qui sert de point central pour gérer les notifications des appplications dans une architecture orientée événements (Event-Driven Architecture). Ce microservice permet de gérer et de récupérer les notifications des différents microservices à travers une API REST OData.

- **OData API** : Fournit une API RESTful utilisant OData pour gérer les notifications.
- **Minimal APIs** : Utilise le modèle léger de Minimal APIs de .NET pour une configuration plus simple.
- **SQlite Database** : Utilise une base de données SQLite pour stocker les notifications.
- **Containerisation** : Conteneurisé à l'aide de Docker pour un déploiement facile.

## Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

## Installation

Clonez ce dépôt et accédez au répertoire du projet :

```bash
git clone https://github.com/brendanGiraudet/ms-notification.git
cd ms-notification
```

## Configuration
Il n'y a pas de configuration nécessaire pour le développement, car le microservice utilise la migration de base de donnée.

Construire et Exécuter le Microservice

1. Construire le Projet
Assurez-vous que vous avez le .NET 8 SDK installé, puis construisez le projet :

```bash
Copier le code
dotnet build
```

2. Exécuter le Projet Localement
Exécutez le microservice localement pour le tester :

```bash
Copier le code
dotnet run
```

Le microservice sera disponible à l'adresse http://localhost:5000 (port specifié dans le fichier appsettings).

3. Utiliser Docker
Construire l'Image Docker et/ou l'éxécuter
Pour construire l'image Docker du microservice, utilisez la commande suivante :

```bash
Copier le code
docker compose -f .\docker-compose-debug.yml up
```

Le service sera disponible à http://localhost:1818 (port specifié dans le fichier docker-compose-debug).

## Utilisation
Points de Terminaison OData
Le microservice expose plusieurs points de terminaison OData pour gérer les notifications.

GET /odata/Notification : Récupère toutes les Notification.
GET /odata/Notification({key}) : Récupère un Log spécifique par ID.
POST /odata/Notification : Ajoute un nouveau Log.

## Exemples de Requêtes avec Postman
Récupérer tous les Notification
Méthode : GET
URL : http://localhost:8080/odata/Notification

## Contribuer
Les contributions sont les bienvenues ! Veuillez soumettre une issue ou une pull request pour toute amélioration ou problème.

## License
Aucune licence

