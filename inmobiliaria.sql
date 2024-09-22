-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 22-09-2024 a las 06:56:47
-- Versión del servidor: 10.4.27-MariaDB
-- Versión de PHP: 8.2.0

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
  `Id_Contrato` int(11) DEFAULT NULL,
  `Id_Pago` int(11) DEFAULT NULL,
  `Accion` varchar(255) NOT NULL,
  `FechaHora` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `auditoria`
--

INSERT INTO `auditoria` (`Id`, `Id_Usuario`, `Id_Contrato`, `Id_Pago`, `Accion`, `FechaHora`) VALUES
(3, 6, 34, NULL, 'Crear Contrato', '2024-09-22 01:42:50'),
(4, 6, NULL, 1, 'Crear pago', '2024-09-22 01:43:08'),
(5, 6, NULL, 2, 'Crear pago', '2024-09-22 01:43:18'),
(6, 6, NULL, 3, 'Crear pago', '2024-09-22 01:43:25'),
(7, 6, NULL, 4, 'Crear pago', '2024-09-22 01:43:31'),
(8, 6, 34, NULL, 'Contrato Finalizado', '2024-09-22 01:43:57'),
(9, 6, 35, NULL, 'Crear Contrato', '2024-09-22 01:46:13'),
(10, 6, 36, NULL, 'Crear Contrato', '2024-09-22 01:47:56'),
(11, 7, NULL, 5, 'Crear pago', '2024-09-22 01:55:00'),
(12, 7, 35, NULL, 'Contrato Finalizado', '2024-09-22 01:55:38');

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

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`Id`, `Id_Inquilino`, `Id_Inmueble`, `FechaInicio`, `FechaFin`, `MontoAlquiler`, `FechaTerminacion`, `Multa`, `Estado`) VALUES
(34, 2, 7, '2024-09-22', '2024-12-22', '90900.00', '2024-12-22', '0.00', 1),
(35, 1, 1, '2025-01-01', '2025-02-01', '155000.00', '2025-02-01', '0.00', 1),
(36, 8, 9, '2025-04-04', '2025-05-04', '111000.00', NULL, NULL, 1);

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
  `Disponible` tinyint(1) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`Id`, `Id_Propietario`, `Id_Tipo`, `Direccion`, `Uso`, `CantidadAmbientes`, `Coordenadas`, `Precio`, `Disponible`, `Estado`) VALUES
(1, 2, 2, 'Rivadavia 103', 'Residencial', 3, '98,12', '150000.00', 1, 1),
(2, 3, 2, 'San Martin 997', 'Residencial', 1, '400, 20', '180000.00', 1, 1),
(4, 4, 3, 'Ayacucho 123', 'Comercial', 2, '31, 12', '310000.00', 1, 1),
(5, 5, 1, 'Maipu 765', 'Residencial', 6, '23, 23', '289000.00', 1, 1),
(6, 6, 1, 'LaFinur 432', 'Comercial', 3, '89, -12.23', '120900.00', 1, 0),
(7, 5, 5, 'Junin 897', 'Residencial', 7, '-64.45, 34.566', '90900.00', 1, 1),
(8, 3, 1, 'Sarmiento 122', 'Comercial', 2, '76, 45', '220800.00', 1, 1),
(9, 4, 1, 'Las Heras 533', 'Residencial', 4, '12, -43.34', '110700.00', 0, 1);

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
(1, '33016244', 'Messi', 'Leonel', '987654432', NULL, 1),
(2, '21543432', 'Ozil', 'Mesut', '+541152799578', NULL, 1),
(3, '28868776', 'Guardiola', 'Pep', '+541150328167', '+541150331632', 1),
(8, '35354321', 'Eto', 'Samuel', '+541152175936', '+541152193398', 1),
(9, '41436534', 'Kross', 'Toni', '+541152799598', NULL, 0);

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

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`Id`, `Id_Contrato`, `NumeroPago`, `FechaPago`, `Detalle`, `Importe`, `Estado`) VALUES
(1, 34, 1, '2024-09-22', 'Septiembre', '90900.00', 1),
(2, 34, 2, '2024-09-22', 'Octubre', '90900.00', 1),
(3, 34, 3, '2024-09-22', 'Noviembre', '90900.00', 1),
(4, 34, 4, '2024-09-22', 'Diciembre', '90900.00', 1),
(5, 35, 1, '2024-09-22', 'Enero', '155000.00', 1);

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
(2, '12345678', 'Sosa', 'Mercedes', '2665323212', 'SL capital, calle 4 n1234', 1),
(3, '12578432', 'Ancelloti', 'Carlo', '+442045770077', 'San Luis, La Punta. m3 c19', 1),
(4, '10191832', 'Mourinho', 'José', '+35 (405) 332-0245', 'san luis capital m5 c6', 1),
(5, '89877878', 'Conte', 'Antonio', '+7504157879', 'Maipu 4432', 1),
(6, '12034187', 'Makelele', 'Claude', '+50598434111', 'San Martin 221', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipos_inmueble`
--

CREATE TABLE `tipos_inmueble` (
  `Id` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipos_inmueble`
--

INSERT INTO `tipos_inmueble` (`Id`, `Descripcion`, `Estado`) VALUES
(1, 'Local', 1),
(2, 'Depósito', 1),
(3, 'Casa', 1),
(4, 'Departamento', 0),
(5, 'Industrial', 1),
(15, 'Finca', 1),
(16, 'Oficina', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Apellido` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Clave` varchar(255) NOT NULL,
  `Avatar` varchar(250) DEFAULT NULL,
  `Rol` int(11) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id`, `Nombre`, `Apellido`, `Email`, `Clave`, `Avatar`, `Rol`, `Estado`) VALUES
(6, 'Kevin', 'Huanca', 'kh@gmail.com', 'jm4zDtM42lOFo88ARAy9VDQB8wxhLWlxTGLavwHbOWQ=', '/img\\avatar_6.jpeg', 1, 1),
(7, 'Nahuel', 'Vargas', 'nv@gmail.com', 'jm4zDtM42lOFo88ARAy9VDQB8wxhLWlxTGLavwHbOWQ=', '/img\\avatar_7.jpeg', 1, 1),
(8, 'Gabriel', 'Torrez', 'gt@gmail.com', 'jm4zDtM42lOFo88ARAy9VDQB8wxhLWlxTGLavwHbOWQ=', '/img\\avatar_8.jpeg', 1, 1),
(9, 'Mariano', 'Luzza', 'ml@gmail.com', 'jm4zDtM42lOFo88ARAy9VDQB8wxhLWlxTGLavwHbOWQ=', '/img\\avatar_9.png', 1, 1),
(10, 'Anders', 'Hejlsberg', 'ah@gmail.com', 'jm4zDtM42lOFo88ARAy9VDQB8wxhLWlxTGLavwHbOWQ=', '/img\\avatar_10.jpeg', 2, 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `auditoria`
--
ALTER TABLE `auditoria`
  ADD PRIMARY KEY (`Id`);

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
  ADD KEY `id_propietario` (`Id_Propietario`) USING BTREE,
  ADD KEY `Id_Tipo` (`Id_Tipo`) USING BTREE;

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
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `tipos_inmueble`
--
ALTER TABLE `tipos_inmueble`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Restricciones para tablas volcadas
--

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
