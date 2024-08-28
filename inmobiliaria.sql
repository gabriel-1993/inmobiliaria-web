-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 28-08-2024 a las 17:24:52
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `auditoria`
--

CREATE TABLE `auditoria` (
  `Id` int(11) NOT NULL,
  `Id_Usuario` int(11) NOT NULL,
  `Accion` varchar(255) NOT NULL,
  `FechaHora` datetime NOT NULL,
  `Detalle` varchar(255) DEFAULT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `Id` int(11) NOT NULL,
  `Id_Inquilino` int(11) NOT NULL,
  `Id_Inmueble` int(11) NOT NULL,
  `FechaInicio` date NOT NULL,
  `FechaFin` date NOT NULL,
  `MontoAlquiler` decimal(10,2) NOT NULL,
  `FechaTerminacion` date DEFAULT NULL,
  `Multa` decimal(10,2) DEFAULT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `Id` int(11) NOT NULL,
  `Id_Propietario` int(11) NOT NULL,
  `Id_Tipo` int(11) NOT NULL,
  `Direccion` varchar(255) NOT NULL,
  `Uso` enum('Comercial','Residencial') NOT NULL,
  `CantidadAmbientes` int(11) NOT NULL,
  `Coordenadas` varchar(100) NOT NULL,
  `Precio` decimal(10,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `Id` int(11) NOT NULL,
  `Dni` varchar(20) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Telefono` varchar(30) NOT NULL,
  `TelefonoSecundario` varchar(30) DEFAULT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`Id`, `Dni`, `Apellido`, `Nombre`, `Telefono`, `TelefonoSecundario`, `Estado`) VALUES
(1, '22222', 'Messi', 'Leonel', '987654432', '987876654', 0),
(2, '321', 'Test', 'Prueba', '123', '456', 1),
(3, '321322', 'Martino', 'Tata', '567567343', '456456546', 1),
(8, '321222', 'asd', 'wewq', '123445', NULL, 0),
(9, '21221', 'qweasd', 'qeww', '234234', NULL, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `Id` int(11) NOT NULL,
  `Id_Contrato` int(11) NOT NULL,
  `NumeroPago` int(11) NOT NULL,
  `FechaPago` date NOT NULL,
  `Detalle` varchar(255) DEFAULT NULL,
  `Importe` decimal(10,2) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `Id` int(11) NOT NULL,
  `Dni` varchar(20) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Telefono` varchar(30) NOT NULL,
  `Direccion` varchar(255) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`Id`, `Dni`, `Apellido`, `Nombre`, `Telefono`, `Direccion`, `Estado`) VALUES
(1, '123459', 'Perez', 'Juan', '2664', 'SL capital, Calle 3 N123', 1),
(2, '1234', 'Sosa', 'Maria', '2665', 'SL capital, calle 4 n1234', 1),
(3, '12567', 'prueba', 'pepe', '1254', 'San Luis, La Punta. m3 c19', 1),
(4, '101918', 'prueba', 'prueba', '12567', 'san luis capital m5 c6', 1),
(5, '89877878', 'jhhjkj', 'jkhkhj', '877878', 'fgjgkhjhl hlhkhkl', 0),
(6, '124343434', 'prueba', 'Ejemplo', '121212', 'asdasd', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipos_inmueble`
--

CREATE TABLE `tipos_inmueble` (
  `Id` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipos_inmueble`
--

INSERT INTO `tipos_inmueble` (`Id`, `Descripcion`) VALUES
(1, 'Local'),
(2, 'Depósito'),
(3, 'Casa'),
(4, 'Departamento');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Clave` varchar(255) NOT NULL,
  `Rol` enum('administrador','empleado') NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `auditoria`
--
ALTER TABLE `auditoria`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `id_usuario` (`Id_Usuario`);

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `id_inmueble` (`Id_Inmueble`),
  ADD KEY `id_inquilino` (`Id_Inquilino`) USING BTREE;

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `id_propietario` (`Id_Propietario`),
  ADD KEY `id_tipo` (`Id_Tipo`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `id_contrato` (`Id_Contrato`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `tipos_inmueble`
--
ALTER TABLE `tipos_inmueble`
  ADD PRIMARY KEY (`Id`) USING BTREE;

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`) USING BTREE;

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `auditoria`
--
ALTER TABLE `auditoria`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `tipos_inmueble`
--
ALTER TABLE `tipos_inmueble`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `auditoria`
--
ALTER TABLE `auditoria`
  ADD CONSTRAINT `auditoria_ibfk_1` FOREIGN KEY (`Id_Usuario`) REFERENCES `usuarios` (`Id`);

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `contratos_ibfk_1` FOREIGN KEY (`Id_Inquilino`) REFERENCES `inquilinos` (`Id`),
  ADD CONSTRAINT `contratos_ibfk_2` FOREIGN KEY (`Id_Inmueble`) REFERENCES `inmuebles` (`Id`);

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`Id_Propietario`) REFERENCES `propietarios` (`Id`),
  ADD CONSTRAINT `inmuebles_ibfk_2` FOREIGN KEY (`Id_Tipo`) REFERENCES `tipos_inmueble` (`Id`);

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `pagos_ibfk_1` FOREIGN KEY (`Id_Contrato`) REFERENCES `contratos` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
