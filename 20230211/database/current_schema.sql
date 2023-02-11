CREATE TABLE generic (
  `id` MEDIUMINT NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (id)
);

CREATE TABLE generic_location (
  `id` MEDIUMINT NOT NULL AUTO_INCREMENT,
  `generic_id` MEDIUMINT,
  `name` LONGTEXT,
  `region` LONGTEXT,
  `country` LONGTEXT,
  `lat` INT,
  `lon` INT,
  `tz_id` LONGTEXT,
  `localtime_epoch` INT,
  `localtime` LONGTEXT,
  PRIMARY KEY (`id`),
  FOREIGN KEY (`generic_id`) REFERENCES generic(`id`)
);

CREATE TABLE generic_current (
  `id` MEDIUMINT NOT NULL AUTO_INCREMENT,
  `generic_id` MEDIUMINT,
  `last_updated_epoch` INT,
  `last_updated` LONGTEXT,
  `temp_c` INT,
  `temp_f` INT,
  `is_day` INT,
  `wind_mph` INT,
  `wind_kph` INT,
  `wind_degree` INT,
  `wind_dir` LONGTEXT,
  `pressure_mb` INT,
  `pressure_in` INT,
  `precip_mm` INT,
  `precip_in` INT,
  `humidity` INT,
  `cloud` INT,
  `feelslike_c` INT,
  `feelslike_f` INT,
  `vis_km` INT,
  `vis_miles` INT,
  `uv` INT,
  `gust_mph` INT,
  `gust_kph` INT,
  PRIMARY KEY (`id`),
  FOREIGN KEY (`generic_id`) REFERENCES generic(`id`)
);

CREATE TABLE generic_current_condition (
  `id` MEDIUMINT NOT NULL AUTO_INCREMENT,
  `generic_current_id` MEDIUMINT,
  `text` LONGTEXT,
  `icon` LONGTEXT,
  `code` INT,
  PRIMARY KEY (`id`),
  FOREIGN KEY (`generic_current_id`) REFERENCES generic_current(`id`)
);