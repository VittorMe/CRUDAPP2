-- MySQL dump 10.13  Distrib 8.0.29, for Win64 (x86_64)
--
-- Host: localhost    Database: testdev
-- ------------------------------------------------------
-- Server version	8.0.29

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `produto`
--

DROP TABLE IF EXISTS `produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produto` (
  `cod` bigint NOT NULL AUTO_INCREMENT,
  `descricao` varchar(100) NOT NULL,
  `codGrupo` int NOT NULL,
  `codBarra` varchar(100) DEFAULT NULL,
  `precoCusto` decimal(11,3) NOT NULL,
  `precoVenda` decimal(11,3) NOT NULL,
  `dataHoraCadastro` datetime NOT NULL,
  `ativo` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`cod`),
  KEY `fk_produtoCodGrupo_idx` (`codGrupo`),
  CONSTRAINT `fk_produtoCodGrupo` FOREIGN KEY (`codGrupo`) REFERENCES `produto_grupo` (`cod`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produto`
--

LOCK TABLES `produto` WRITE;
/*!40000 ALTER TABLE `produto` DISABLE KEYS */;
INSERT INTO `produto` VALUES (1,'REQUEIJAO',5,NULL,2.500,3.790,'2020-07-01 16:16:01',1),(2,'PAO FRANCES',4,NULL,1.250,2.500,'2020-07-01 16:16:01',1),(3,'BANDEJA DE QUEIJOS (500G)',5,NULL,7.900,15.500,'2020-07-01 16:16:01',1),(4,'BANDEJA - PEITO DE FRANGO 1KG',2,NULL,4.600,12.900,'2020-07-01 16:16:01',1),(5,'AGUA COM GAS BONAFONTE',1,'7891025110941',1.200,3.000,'2020-07-01 16:16:01',1);
/*!40000 ALTER TABLE `produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produto_grupo`
--

DROP TABLE IF EXISTS `produto_grupo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `produto_grupo` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produto_grupo`
--

LOCK TABLES `produto_grupo` WRITE;
/*!40000 ALTER TABLE `produto_grupo` DISABLE KEYS */;
INSERT INTO `produto_grupo` VALUES (1,'GERAL'),(2,'AÇOUGUE'),(3,'HORTIFRUTI'),(4,'PADARIA'),(5,'GELADEIRA');
/*!40000 ALTER TABLE `produto_grupo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `venda`
--

DROP TABLE IF EXISTS `venda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `venda` (
  `cod` bigint NOT NULL AUTO_INCREMENT,
  `clienteDocumento` varchar(18) DEFAULT NULL,
  `clienteNome` varchar(50) DEFAULT NULL,
  `obs` varchar(300) DEFAULT NULL,
  `total` decimal(11,2) NOT NULL,
  `dataHora` datetime NOT NULL,
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `venda`
--

LOCK TABLES `venda` WRITE;
/*!40000 ALTER TABLE `venda` DISABLE KEYS */;
INSERT INTO `venda` VALUES (1,'763.158.830-95','Lúcio',NULL,15.50,'2020-07-01 16:21:20'),(2,NULL,'João','Quando o pão ficar pronto levar na mesa',5.00,'2020-07-01 16:21:20'),(3,NULL,NULL,NULL,12.79,'2020-07-01 16:21:20');
/*!40000 ALTER TABLE `venda` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `venda_produto`
--

DROP TABLE IF EXISTS `venda_produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `venda_produto` (
  `cod` bigint NOT NULL AUTO_INCREMENT,
  `codVenda` bigint NOT NULL,
  `codProduto` bigint NOT NULL,
  `precoVenda` decimal(11,3) NOT NULL,
  `quantidade` decimal(11,2) NOT NULL,
  PRIMARY KEY (`cod`),
  KEY `fk_vendaprodutoCodProduto_idx` (`codProduto`),
  KEY `fk_vendaprodutoCodVenda_idx` (`codVenda`),
  CONSTRAINT `fk_vendaprodutoCodProduto` FOREIGN KEY (`codProduto`) REFERENCES `produto` (`cod`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `fk_vendaprodutoCodVenda` FOREIGN KEY (`codVenda`) REFERENCES `venda` (`cod`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `venda_produto`
--

LOCK TABLES `venda_produto` WRITE;
/*!40000 ALTER TABLE `venda_produto` DISABLE KEYS */;
INSERT INTO `venda_produto` VALUES (1,1,3,15.500,1.00),(2,2,2,2.500,2.00),(3,3,5,3.000,3.00),(4,3,1,3.790,1.00);
/*!40000 ALTER TABLE `venda_produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'testdev'
--

--
-- Dumping routines for database 'testdev'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-06-03 18:32:29
