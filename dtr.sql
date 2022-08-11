/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50736
Source Host           : localhost:3306
Source Database       : dtr

Target Server Type    : MYSQL
Target Server Version : 50736
File Encoding         : 65001

Date: 2022-08-11 11:27:15
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for dtr
-- ----------------------------
DROP TABLE IF EXISTS `dtr`;
CREATE TABLE `dtr` (
  `dtr_id` int(11) NOT NULL,
  `employee_id` int(11) DEFAULT NULL,
  `dtr_morning_in` varchar(255) DEFAULT NULL,
  `dtr_morning_out` varchar(255) DEFAULT NULL,
  `dtr_afternoon_in` varchar(255) DEFAULT NULL,
  `dtr_afternoon_out` varchar(255) DEFAULT NULL,
  `dtr_overtime_in` varchar(255) DEFAULT NULL,
  `dtr_overtime_out` varchar(255) DEFAULT NULL,
  `dtr_date` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`dtr_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of dtr
-- ----------------------------
INSERT INTO `dtr` VALUES ('1', '4', '8:42AM', '11:32 PM', '02:32 PM', '05:32 PM', '05:32 PM', '08:32 PM', '4/12/2022');

-- ----------------------------
-- Table structure for employee
-- ----------------------------
DROP TABLE IF EXISTS `employee`;
CREATE TABLE `employee` (
  `employee_id` int(11) NOT NULL,
  `employee_name` varchar(255) DEFAULT NULL,
  `employee_dtr_password` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`employee_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of employee
-- ----------------------------
INSERT INTO `employee` VALUES ('4', 'rose cembrano', '123');

-- ----------------------------
-- Table structure for staff
-- ----------------------------
DROP TABLE IF EXISTS `staff`;
CREATE TABLE `staff` (
  `staff_id` int(11) NOT NULL,
  `staff_username` varchar(255) DEFAULT NULL,
  `staff_password` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`staff_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of staff
-- ----------------------------
INSERT INTO `staff` VALUES ('1', 'admin', '321');
