CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS cults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  name VARCHAR(255) NOT NULL,
  description TEXT NOT NULL,
  fee INT NOT NULL,
  coverImg VARCHAR(500) NOT NULL,
  leaderId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  FOREIGN KEY (leaderId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE cults;


CREATE TABLE IF NOT EXISTS cultMembers(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  accountId VARCHAR(255) NOT NULL,
  cultId INT NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (cultId) REFERENCES cults(id) ON DELETE CASCADE,
  -- You can only join a cult once
  UNIQUE (cultId, accountId)
)default charset utf8 COMMENT '';

SELECT
    c.*,
    COUNT(cm.id) AS cultistCount,
    acc.*
    FROM cults c
    LEFT JOIN cultMembers cm ON cm.cultId = c.id
    JOIN accounts acc ON acc.id = c.leaderId
    GROUP BY c.id;
    