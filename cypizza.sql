-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Anamakine: 127.0.0.1:3308
-- Üretim Zamanı: 05 Oca 2025, 13:52:38
-- Sunucu sürümü: 10.4.32-MariaDB
-- PHP Sürümü: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `cypizza`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `employers`
--

CREATE TABLE `employers` (
  `e_id` int(11) NOT NULL,
  `e_name` varchar(255) DEFAULT NULL,
  `e_surname` varchar(255) DEFAULT NULL,
  `job` varchar(255) DEFAULT NULL,
  `e_mail` varchar(255) DEFAULT NULL,
  `p_number` varchar(255) DEFAULT NULL,
  `salary` double DEFAULT 35000
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Tablo döküm verisi `employers`
--

INSERT INTO `employers` (`e_id`, `e_name`, `e_surname`, `job`, `e_mail`, `p_number`, `salary`) VALUES
(1, 'Emrah', 'Yurdusev', 'admin', 'emrah@emrah.com', '', 35000),
(2, 'ahmet', 'emrah', 'CASHIER', 'ahmwd', '05333333333', 35000),
(23, '2094', 'köle', 'CHEF', 'd', 'ad', 35000),
(28, 'mehmet', 'ıııı', 'STOCK', 'mehmet123', '2131231', 35000);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `entry`
--

CREATE TABLE `entry` (
  `e_id` int(11) NOT NULL,
  `password` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Tablo döküm verisi `entry`
--

INSERT INTO `entry` (`e_id`, `password`) VALUES
(1, '123'),
(2, '123'),
(23, '123'),
(28, '123');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `orders`
--

CREATE TABLE `orders` (
  `o_id` int(11) DEFAULT NULL,
  `pr_name` varchar(255) DEFAULT NULL,
  `type` varchar(255) DEFAULT NULL,
  `unit` varchar(255) DEFAULT NULL,
  `situation` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Tablo döküm verisi `orders`
--

INSERT INTO `orders` (`o_id`, `pr_name`, `type`, `unit`, `situation`) VALUES
(1, 'Margherita', 'menü', '2', 'finished'),
(1, 'ketchup', 'extra', '1', 'finished'),
(1, 'cola', 'drink', '1', 'finished'),
(3, 'Margherita', 'menü', '8', 'finished'),
(4, 'Margherita', 'menü', '2', 'finished'),
(5, 'Margherita', 'menü', '1', 'finished'),
(7, 'Margherita', 'menü', '1', 'finished'),
(8, 'Margherita', 'menü', '8', 'finished'),
(8, 'Margherita', 'menü', '8', 'finished'),
(8, 'Margherita', 'menü', '1', 'finished'),
(11, 'Margherita', 'menü', '1', 'finished');

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `products`
--

CREATE TABLE `products` (
  `pr_id` int(11) NOT NULL,
  `pr_name` varchar(255) DEFAULT NULL,
  `type` varchar(255) DEFAULT NULL,
  `unit` int(11) DEFAULT NULL,
  `price` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Tablo döküm verisi `products`
--

INSERT INTO `products` (`pr_id`, `pr_name`, `type`, `unit`, `price`) VALUES
(11, 'margherita', 'menu', 0, '50'),
(12, 'cola', 'drink', 7, '20'),
(13, 'ketchup', 'extra', 8, '10');

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `employers`
--
ALTER TABLE `employers`
  ADD PRIMARY KEY (`e_id`),
  ADD UNIQUE KEY `p_number` (`p_number`),
  ADD UNIQUE KEY `e_mail` (`e_mail`);

--
-- Tablo için indeksler `entry`
--
ALTER TABLE `entry`
  ADD PRIMARY KEY (`e_id`);

--
-- Tablo için indeksler `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`pr_id`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `employers`
--
ALTER TABLE `employers`
  MODIFY `e_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- Tablo için AUTO_INCREMENT değeri `entry`
--
ALTER TABLE `entry`
  MODIFY `e_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- Tablo için AUTO_INCREMENT değeri `products`
--
ALTER TABLE `products`
  MODIFY `pr_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- Dökümü yapılmış tablolar için kısıtlamalar
--

--
-- Tablo kısıtlamaları `entry`
--
ALTER TABLE `entry`
  ADD CONSTRAINT `entry_ibfk_1` FOREIGN KEY (`e_id`) REFERENCES `employers` (`e_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
