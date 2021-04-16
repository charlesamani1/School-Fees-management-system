-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 16, 2021 at 04:39 PM
-- Server version: 10.4.6-MariaDB
-- PHP Version: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sfms`
--

-- --------------------------------------------------------

--
-- Table structure for table `fee`
--

CREATE TABLE `fee` (
  `Fee_Id` int(11) NOT NULL,
  `Yos` varchar(50) NOT NULL,
  `Semister` varchar(50) NOT NULL,
  `program` varchar(50) NOT NULL,
  `Examination` float NOT NULL,
  `Tution` float NOT NULL,
  `Medical` float NOT NULL,
  `Activity` float NOT NULL,
  `Amenity` float NOT NULL,
  `Amount` float NOT NULL,
  `Accademic_Year` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `fee`
--

INSERT INTO `fee` (`Fee_Id`, `Yos`, `Semister`, `program`, `Examination`, `Tution`, `Medical`, `Activity`, `Amenity`, `Amount`, `Accademic_Year`) VALUES
(1, 'I', 'I', 'CSC', 1000, 1000, 1000, 1000, 1000, 6000, '2017/2018'),
(2, 'I', 'II', 'CSC', 2000, 2000, 2000, 2000, 2000, 10000, '2017/2018'),
(3, 'II', 'I', 'CSC', 3000, 3000, 3000, 3000, 3000, 15000, '2017/2018'),
(4, 'II', 'II', 'CSC', 100, 100, 100, 100, 100, 500, '2017/2018'),
(5, 'III', 'I', 'CSC', 200, 200, 200, 200, 200, 1000, '2017/2018'),
(6, 'III', 'II', 'CSC', 300, 300, 300, 300, 300, 15000, '2017/2018'),
(7, 'IV', 'I', 'CSC', 2200, 2200, 2200, 2200, 2200, 11000, '2017/2018'),
(8, 'IV', 'II', 'CSC', 1200, 1200, 1200, 1200, 1200, 6000, '2017/2018');

-- --------------------------------------------------------

--
-- Table structure for table `program`
--

CREATE TABLE `program` (
  `Program_Code` varchar(60) NOT NULL DEFAULT '',
  `Program_Name` varchar(255) DEFAULT NULL,
  `Department_Code` varchar(60) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `program`
--

INSERT INTO `program` (`Program_Code`, `Program_Name`, `Department_Code`, `Description`) VALUES
('BIT', 'Bsc. Information Technology', 'IT', '4 Years'),
('CSC', 'Bsc. Computer Science', 'CSC', '4 Years'),
('ECE', 'Bsc. Electrical and Communication Engineering', 'ECE', '5 Years'),
('MIE', 'Bsc. Mechanical and Industrial Engineering', 'MIE', '5 Years');

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE `role` (
  `Role_Id` int(11) NOT NULL,
  `Role_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`Role_Id`, `Role_name`) VALUES
(1, 'Administrator'),
(2, 'Accountant');

-- --------------------------------------------------------

--
-- Table structure for table `staff`
--

CREATE TABLE `staff` (
  `Staff_No` varchar(10) NOT NULL,
  `Pf_No` varchar(10) NOT NULL,
  `Staff_Name` varchar(255) NOT NULL,
  `Designation` varchar(60) NOT NULL,
  `Department_Code` varchar(60) DEFAULT NULL,
  `Staff_ID` varchar(50) NOT NULL,
  `Postal_Address` varchar(20) NOT NULL,
  `Postal_Code` varchar(10) NOT NULL,
  `Town` varchar(60) NOT NULL,
  `Residence` varchar(255) NOT NULL,
  `Email_Address` varchar(60) NOT NULL,
  `Phone_No` varchar(60) NOT NULL,
  `Office` varchar(60) NOT NULL,
  `Time_Available` varchar(60) NOT NULL,
  `Login_Name` varchar(26) NOT NULL,
  `Passsword` varchar(60) NOT NULL,
  `Priviledges` varchar(30) NOT NULL DEFAULT 'Staff',
  `Reg_Date` timestamp NOT NULL DEFAULT current_timestamp(),
  `Status` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `staff`
--

INSERT INTO `staff` (`Staff_No`, `Pf_No`, `Staff_Name`, `Designation`, `Department_Code`, `Staff_ID`, `Postal_Address`, `Postal_Code`, `Town`, `Residence`, `Email_Address`, `Phone_No`, `Office`, `Time_Available`, `Login_Name`, `Passsword`, `Priviledges`, `Reg_Date`, `Status`) VALUES
('EMP/0001', '980', 'Charles Amani', 'ADMINISTRATOR', 'CSC', '12012', '645', '21543', 'Califonia', 'Florida', 'charlesamani@gmail.com', '0707153062', 'LBB 014', 'friday 8.00-11.00', 'Admin', 'Admin', 'Administrator', '2017-12-19 23:05:37', 1),
('EMP/0002', '678', 'Samuel Baraka', 'ACCOUNTANT', 'CVS', '54541', '213', '78451', 'Mombasa', 'Bamburi', 'samuelbaraka@gmail.com', '0712457896', 'lbb 014', 'everyday', 'Accountant', 'Accountant', 'Accountant', '2017-12-19 23:35:50', 1);

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `Reg_No` varchar(20) NOT NULL,
  `Student_Name` varchar(255) NOT NULL,
  `Program_Code` varchar(60) NOT NULL,
  `Semester` varchar(60) NOT NULL,
  `Year_of_Study` varchar(50) NOT NULL,
  `Postal_Address` varchar(20) NOT NULL,
  `Postal_Code` varchar(10) NOT NULL,
  `Town` varchar(60) NOT NULL,
  `Residence` varchar(255) NOT NULL,
  `Email_Address` varchar(60) NOT NULL,
  `Phone_No` varchar(60) NOT NULL,
  `Status` int(1) NOT NULL DEFAULT 0
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`Reg_No`, `Student_Name`, `Program_Code`, `Semester`, `Year_of_Study`, `Postal_Address`, `Postal_Code`, `Town`, `Residence`, `Email_Address`, `Phone_No`, `Status`) VALUES
('', 'retgetr', 'CSC', 'I', 'I', '345', '123345', 'dsgd', 'edd', 'c@gmail.com', '0707070707', 1),
('COM/0059/14', 'Samuel B. Nzai', 'CSC', 'II', 'IV', '545', '546', 'Kisumu', 'kisumu city', 'samuelbaraka@gmail.com', '+254757869415', 1),
('COM/0001/15', 'Josphine I. Nzai', 'CSC', 'I', 'III', '454', '45221', 'KISII', 'Njoro', 'josphenImani@gmail.com', '+254714253698', 1),
('COM/0002/15', 'Solomoni Mvera', 'CSC', 'II', 'III', '152', '12221', 'Bungoma', 'kibabii', 'solomonimvera@gmail.com', '+254712369857', 1),
('COM/2246/15', 'Debora Kadzo', 'CSC', 'I', 'II', '545', '12545', 'KILIFI', 'KILIFI BAY', 'deborakadzo@gmail.com', '+254702365897', 1),
('rtgt', 'retgetr', 'CSC', 'I', 'I', '', '', '', '', '', '', 1);

-- --------------------------------------------------------

--
-- Table structure for table `transaction`
--

CREATE TABLE `transaction` (
  `Transaction_Id` int(11) NOT NULL,
  `Transaction_Date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `Stu_Reg` varchar(250) NOT NULL,
  `Stu_Name` varchar(250) NOT NULL,
  `Total_Paid` float NOT NULL,
  `Balance` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `transaction`
--

INSERT INTO `transaction` (`Transaction_Id`, `Transaction_Date`, `Stu_Reg`, `Stu_Name`, `Total_Paid`, `Balance`) VALUES
(1, '2017-12-20 01:40:29', 'COM/2246/15', 'Debora Kadzo', 15000, 0),
(2, '2017-12-20 02:18:13', 'COM/2246/15', 'Debora Kadzo', 1200, 13800),
(3, '2017-12-20 02:18:29', 'COM/0002/15', 'Solomoni Mvera', 15000, 0),
(4, '2017-12-20 02:20:56', 'COM/0058/14', 'Charles N. Amani', 1100, 9900),
(5, '2017-12-20 02:21:12', 'COM/0059/14', 'Samuel B. Nzai', 6000, 0),
(6, '2017-12-20 02:21:29', 'COM/0001/15', 'Josphine I. Nzai', 1000, 0),
(7, '2017-12-25 23:16:37', 'COM/0058/14', 'Charles N. Amani', 1000, 10000),
(8, '2020-11-26 16:40:18', 'COM/0058/14', 'Charles N. Amani', 1200, 9800),
(9, '2021-04-16 13:59:12', 'COM/0058/14', 'Charles N. Amani', 123, 10877),
(10, '2021-04-16 14:23:03', 'COM/0058/14', 'Charles N. Amani', 145, 10855);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `fee`
--
ALTER TABLE `fee`
  ADD PRIMARY KEY (`Fee_Id`);

--
-- Indexes for table `program`
--
ALTER TABLE `program`
  ADD PRIMARY KEY (`Program_Code`);

--
-- Indexes for table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`Role_Id`);

--
-- Indexes for table `staff`
--
ALTER TABLE `staff`
  ADD PRIMARY KEY (`Staff_No`),
  ADD KEY `DepartmentCode` (`Department_Code`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`Reg_No`);

--
-- Indexes for table `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`Transaction_Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `fee`
--
ALTER TABLE `fee`
  MODIFY `Fee_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `Role_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `transaction`
--
ALTER TABLE `transaction`
  MODIFY `Transaction_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
