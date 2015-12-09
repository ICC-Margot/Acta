-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 09-12-2015 a las 21:49:00
-- Versión del servidor: 5.6.17
-- Versión de PHP: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `acta`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `alum_pract`
--

CREATE TABLE IF NOT EXISTS `alum_pract` (
  `id_Alumno` int(80) NOT NULL,
  `id_Practica` int(80) NOT NULL,
  KEY `id_Alumno` (`id_Alumno`),
  KEY `id_Practica` (`id_Practica`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `alum_proy`
--

CREATE TABLE IF NOT EXISTS `alum_proy` (
  `id_Alumno` int(80) NOT NULL,
  `id_Proyecto` int(80) NOT NULL,
  `Calificacion` double NOT NULL,
  KEY `id_Alumno` (`id_Alumno`),
  KEY `id_Proyecto` (`id_Proyecto`),
  KEY `id_Proyecto_2` (`id_Proyecto`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `alumno`
--

CREATE TABLE IF NOT EXISTS `alumno` (
  `id_Alumno` int(80) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  `ApellidoP` varchar(50) NOT NULL,
  `ApellidoM` varchar(50) NOT NULL,
  PRIMARY KEY (`id_Alumno`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `criterios`
--

CREATE TABLE IF NOT EXISTS `criterios` (
  `id_E` int(1) NOT NULL,
  `P_Practicas` int(1) NOT NULL,
  `P_Proyectos` int(1) NOT NULL,
  PRIMARY KEY (`id_E`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `criterios`
--

INSERT INTO `criterios` (`id_E`, `P_Practicas`, `P_Proyectos`) VALUES
(0, 40, 60);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `practicas`
--

CREATE TABLE IF NOT EXISTS `practicas` (
  `id_Practica` int(80) NOT NULL AUTO_INCREMENT,
  `Nombre_Practica` varchar(80) NOT NULL,
  PRIMARY KEY (`id_Practica`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Volcado de datos para la tabla `practicas`
--

INSERT INTO `practicas` (`id_Practica`, `Nombre_Practica`) VALUES
(1, 'Polimorfismo'),
(2, 'Binario'),
(3, 'Invernadero'),
(5, 'Calculadora'),
(6, 'suma500'),
(7, 'matrices'),
(8, 'matrizcol');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proyecto`
--

CREATE TABLE IF NOT EXISTS `proyecto` (
  `id_Proyecto` int(80) NOT NULL AUTO_INCREMENT,
  `Nombre_Proyecto` varchar(80) NOT NULL,
  PRIMARY KEY (`id_Proyecto`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Volcado de datos para la tabla `proyecto`
--

INSERT INTO `proyecto` (`id_Proyecto`, `Nombre_Proyecto`) VALUES
(1, 'signos vitales'),
(2, 'Calificaciones');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `puntosmenos`
--

CREATE TABLE IF NOT EXISTS `puntosmenos` (
  `id_Alumno` int(80) NOT NULL,
  `Menos` double NOT NULL,
  KEY `id_Alumno` (`id_Alumno`),
  KEY `id_Alumno_2` (`id_Alumno`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `alum_pract`
--
ALTER TABLE `alum_pract`
  ADD CONSTRAINT `alum_pract_ibfk_1` FOREIGN KEY (`id_Alumno`) REFERENCES `alumno` (`id_Alumno`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `alum_pract_ibfk_2` FOREIGN KEY (`id_Practica`) REFERENCES `practicas` (`id_Practica`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `alum_proy`
--
ALTER TABLE `alum_proy`
  ADD CONSTRAINT `alum_proy_ibfk_1` FOREIGN KEY (`id_Alumno`) REFERENCES `alumno` (`id_Alumno`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `alum_proy_ibfk_2` FOREIGN KEY (`id_Proyecto`) REFERENCES `proyecto` (`id_Proyecto`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `puntosmenos`
--
ALTER TABLE `puntosmenos`
  ADD CONSTRAINT `puntosmenos_ibfk_1` FOREIGN KEY (`id_Alumno`) REFERENCES `alumno` (`id_Alumno`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
