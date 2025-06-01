-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : dim. 01 juin 2025 à 09:28
-- Version du serveur : 8.3.0
-- Version de PHP : 8.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `gestion_pharmacie`
--

-- --------------------------------------------------------

--
-- Structure de la table `achat`
--

DROP TABLE IF EXISTS `achat`;
CREATE TABLE IF NOT EXISTS `achat` (
  `numAchat` varchar(10) NOT NULL,
  `nomClient` varchar(100) NOT NULL,
  `dateAchat` date NOT NULL,
  PRIMARY KEY (`numAchat`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `achat`
--

INSERT INTO `achat` (`numAchat`, `nomClient`, `dateAchat`) VALUES
('ACH001', 'Jean Dupont', '2025-04-26'),
('ACH002', 'Claudia', '2025-04-27'),
('ACH003', 'Emmanuel', '2025-04-27'),
('ACH004', 'Emile', '2025-04-28'),
('ACH005', 'Andry', '2025-05-02'),
('ACH006', 'Kone', '2025-05-02'),
('ACH007', 'Ony', '2025-05-03'),
('ACH008', 'Tody', '2025-05-04'),
('ACH009', 'Mampionona', '2025-05-04'),
('ACH010', 'Farao', '2025-05-04'),
('ACH011', 'Nicki', '2025-05-04'),
('ACH012', 'Lando', '2025-05-05'),
('ACH013', 'Hubert', '2025-05-05'),
('ACH014', 'JAH', '2025-05-05'),
('ACH015', 'Nory', '2025-05-05');

-- --------------------------------------------------------

--
-- Structure de la table `achat_details`
--

DROP TABLE IF EXISTS `achat_details`;
CREATE TABLE IF NOT EXISTS `achat_details` (
  `id` int NOT NULL AUTO_INCREMENT,
  `numAchat` varchar(10) NOT NULL,
  `numMedoc` varchar(10) NOT NULL,
  `quantite` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `numAchat` (`numAchat`),
  KEY `numMedoc` (`numMedoc`)
) ;

--
-- Déchargement des données de la table `achat_details`
--

INSERT INTO `achat_details` (`id`, `numAchat`, `numMedoc`, `quantite`) VALUES
(4, 'ACH001', 'M003', 1),
(3, 'ACH001', 'M001', 2),
(6, 'ACH002', 'M003', 4),
(10, 'ACH003', 'M003', 1),
(9, 'ACH003', 'M002', 3),
(11, 'ACH003', 'M002', 1),
(12, 'ACH004', 'M006', 2),
(13, 'ACH005', 'M003', 1),
(14, 'ACH006', 'M006', 3),
(16, 'ACH007', 'M002', 4),
(17, 'ACH008', 'M005', 1),
(18, 'ACH008', 'M001', 1),
(19, 'ACH009', 'M003', 1),
(20, 'ACH010', 'M001', 1),
(22, 'ACH011', 'M001', 3),
(24, 'ACH012', 'M005', 1),
(26, 'ACH013', 'M003', 3),
(27, 'ACH014', 'M007', 10),
(28, 'ACH015', 'M003', 3);

-- --------------------------------------------------------

--
-- Structure de la table `entree`
--

DROP TABLE IF EXISTS `entree`;
CREATE TABLE IF NOT EXISTS `entree` (
  `numEntree` varchar(10) NOT NULL,
  `numMedoc` varchar(10) NOT NULL,
  `stockEntree` int NOT NULL,
  `dateEntree` date NOT NULL,
  PRIMARY KEY (`numEntree`),
  KEY `numMedoc` (`numMedoc`)
) ;

--
-- Déchargement des données de la table `entree`
--

INSERT INTO `entree` (`numEntree`, `numMedoc`, `stockEntree`, `dateEntree`) VALUES
('E001', 'M001', 60, '2025-04-25'),
('E002', 'M002', 50, '2025-05-05'),
('E003', 'M003', 50, '2025-05-06'),
('E004', 'M005', 60, '2025-05-05'),
('E005', 'M007', 200, '2025-05-05'),
('E006', 'M001', 78, '2025-05-05'),
('E007', 'M001', 30, '2025-05-05');

-- --------------------------------------------------------

--
-- Structure de la table `medicament`
--

DROP TABLE IF EXISTS `medicament`;
CREATE TABLE IF NOT EXISTS `medicament` (
  `numMedoc` varchar(10) NOT NULL,
  `Design` varchar(100) NOT NULL,
  `prix_unitaire` int NOT NULL,
  `stock` int NOT NULL DEFAULT '0',
  `datePeremption` date NOT NULL,
  PRIMARY KEY (`numMedoc`)
) ;

--
-- Déchargement des données de la table `medicament`
--

INSERT INTO `medicament` (`numMedoc`, `Design`, `prix_unitaire`, `stock`, `datePeremption`) VALUES
('M001', 'Eferegan', 1000, 170, '2025-03-15'),
('M003', 'Amoxiline', 500, 63, '2028-04-30'),
('M005', 'Tetraciline', 1200, 71, '2026-05-01'),
('M006', 'Métronidazole', 1200, 3, '2026-11-14'),
('M007', 'Vitamine C', 800, 390, '2025-05-30'),
('M008', 'Aspirine', 500, 50, '2025-05-01'),
('M009', 'pertamole', 5000, 150, '2025-05-05');

-- --------------------------------------------------------

--
-- Structure de la table `utilisateur`
--

DROP TABLE IF EXISTS `utilisateur`;
CREATE TABLE IF NOT EXISTS `utilisateur` (
  `idUtilisateur` int NOT NULL AUTO_INCREMENT,
  `nomUtilisateur` varchar(50) NOT NULL,
  `motDePasse` varchar(255) NOT NULL,
  `role` enum('Principal','Simple') NOT NULL,
  `nomComplet` varchar(100) DEFAULT NULL,
  `dateCreation` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`idUtilisateur`),
  UNIQUE KEY `nomUtilisateur` (`nomUtilisateur`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `utilisateur`
--

INSERT INTO `utilisateur` (`idUtilisateur`, `nomUtilisateur`, `motDePasse`, `role`, `nomComplet`, `dateCreation`) VALUES
(1, 'admin', 'admin123', 'Principal', 'Administrateur Principal', '2025-05-04 23:32:17'),
(2, 'user1', 'user123', 'Simple', 'Utilisateur Standard', '2025-05-04 23:32:17');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
