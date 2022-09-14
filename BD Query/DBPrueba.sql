-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1:3306
-- Tiempo de generación: 18-08-2022 a las 16:40:26
-- Versión del servidor: 8.0.21
-- Versión de PHP: 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `dp_prueba`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE PROCEDURE `Usersp_eliminar` (IN `id` INT(6))  BEGIN
DELETE FROM usuario WHERE Token = id;
END$$

CREATE PROCEDURE `Usersp_listar` ()  begin
select * from usuario;
end$$

CREATE PROCEDURE `Usersp_modificar` (IN `id` INT, IN `user` VARCHAR(30), IN `password` VARCHAR(30))  begin 
Update usuario SET User = user, Password = password WHERE Token = id; end$$

CREATE PROCEDURE `Usersp_obtener` (IN `usuario` VARCHAR(40), IN `contrasena` VARCHAR(40))  begin
select * from usuario where User = usuario AND Password = contrasena;
end$$

CREATE PROCEDURE `Usersp_registrar` (IN `token` INT, IN `user` VARCHAR(30), IN `password` VARCHAR(30), IN `tipoUsuario` INT)  begin 
insert into usuario(Token,User,Password,Tipo_Usuario) 
values ( token, user, password, tipoUsuario); end$$

CREATE  PROCEDURE `Usersp_token` (IN `id` INT)  BEGIN
select * from usuario where Token = id;
END$$

CREATE PROCEDURE `usp_eliminar` (IN `id` INT)  begin

delete from registro where Token = id;

end$$

CREATE PROCEDURE `usp_listar` ()  begin
select * from registro;
end$$

CREATE PROCEDURE `usp_modificar` (IN `id` INT, IN `nombre` VARCHAR(30), IN `apellidos` VARCHAR(30), IN `user` VARCHAR(30), IN `password` VARCHAR(30), IN `correo` VARCHAR(60), IN `fechadenacimiento` VARCHAR(40), IN `sexo` VARCHAR(10), IN `foto` TEXT)  begin 
UPDATE registro set Nombre = nombre,
Apellidos = apellidos,
User = user,
Password = password,
Correo = correo,
Fechadenacimiento = fechadenacimiento, 
Sexo =sexo,
Foto =foto
WHERE Token = id; 
end$$

CREATE PROCEDURE `usp_obtener` (IN `usuario` VARCHAR(40))  begin
select * from registro where User = usuario;
end$$

CREATE PROCEDURE `usp_registrar` (IN `token` INT, IN `nombre` VARCHAR(30), IN `apellidos` VARCHAR(30), IN `user` VARCHAR(30), IN `password` VARCHAR(30), IN `correo` VARCHAR(60), IN `fechadenacimiento` VARCHAR(40), IN `sexo` VARCHAR(10), IN `foto` LONGTEXT)  begin

insert into registro(Token,Nombre,Apellidos,User,Password,Correo,Fechadenacimiento,Sexo,Foto)
values
(
token,
nombre,
apellidos,
user,
password,    
correo,
fechadenacimiento,
sexo,
foto
);
end$$

CREATE PROCEDURE `usp_token` (IN `id` INT)  BEGIN
select * from registro where Token = id;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `registro`
--

CREATE TABLE IF NOT EXISTS `registro` (
  `Token` int NOT NULL,
  `Nombre` varchar(30) NOT NULL,
  `Apellidos` varchar(30) NOT NULL,
  `User` varchar(30) NOT NULL,
  `Password` varchar(30) NOT NULL,
  `Correo` varchar(50) NOT NULL,
  `Fechadenacimiento` datetime NOT NULL,
  `Sexo` varchar(10) NOT NULL,
  `Foto` longtext,
  PRIMARY KEY (`Token`)
);

--
-- Volcado de datos para la tabla `registro`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE IF NOT EXISTS `usuario` (
  `Token` int NOT NULL,
  `User` varchar(30) NOT NULL,
  `Password` varchar(30) NOT NULL,
  `Tipo_Usuario` tinyint(1) NOT NULL,
  PRIMARY KEY (`Token`)
);

--
-- Volcado de datos para la tabla `usuario`
--


/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
