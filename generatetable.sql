

DROP TABLE IF EXISTS password_hash;
DROP TABLE IF EXISTS customer_buy;
DROP TABLE IF EXISTS avatar;
DROP TABLE IF EXISTS ordre;
DROP TABLE IF EXISTS users;


create table users
(
    id         SERIAL PRIMARY KEY,
    full_name  VARCHAR(50)  NOT NULL,
    street     VARCHAR(50)   not NULL,
    zip        integer   not NULL,
    email      VARCHAR(50)  NOT NULL UNIQUE,
    admin      BOOLEAN      NOT NULL CHECK (admin IN (FALSE, TRUE)) DEFAULT FALSE
);

INSERT INTO users (full_name, street, zip,email,  admin)
VALUES ('Joe Doe', 'A vej', '6710','test@example.com', true);



create table avatar
(
    avatar_id     SERIAL PRIMARY KEY,
    avatar_name  VARCHAR(50)  NOT NULL,
    price        integer   not NULL
    );

INSERT INTO avatar (avatar_name,price)
VALUES ('Nummer1', 10);

create table ordre
(
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users (id)
);
INSERT INTO ordre (user_id)
VALUES (1);


create table customer_buy
(
    id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NUll,
    avatar_id INTEGER NOT NULL,
    FOREIGN KEY (order_id) REFERENCES ordre (id),
    FOREIGN KEY (avatar_id) REFERENCES avatar (avatar_id)
);
INSERT INTO customer_buy (order_id, avatar_id) 
VALUES (1,1); 


create table password_hash
(
    user_id   integer      NOT NULL,
    hash      VARCHAR(350) NOT NULL,
    salt      VARCHAR(180) NOT NULL,
    algorithm VARCHAR(12)  NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users (id)
);

INSERT INTO password_hash
VALUES ((SELECT id FROM users WHERE email = 'test@example.com'),
        'Wtk1t9JP2RIJGX9w0mteJs3FUpUR/Da9fZ0k1CNyMTaLLRKcprlGnuiLiTweq5jwZe80nGY5p51jqUERV2rJ+OoWiJhapssHK2uEzHUIpgs3LKLSxctk/czdGQbhr5YWwo4tpQvczx1tgSrV1CZ3rVaZT38Pc/xDABz21+QezAlnstdyDVfY0Hkj7/mWQ39Z6C4EAXb3V45T3gXq+D6pMAbVtMmQ2SQv7rfj9vJDV4h+z7MWzMO+5emffRg561+reZuCytnCYEt/a+5YkNdQHXtnY1RbuhaAF67Ulj2CtVL4hmcePR5HVm6Molyv+s7bxUGHJmzBbl5/9hJdsTh7zg==',
        'KWmoAN50Z0dSh4HAZ2H+2r5apJ5weqi9Q4HkOPFBf4EcDIPET6vBFBh3d99Y9Hd6kpNOr/INIY2+zHX75gGTWQ5FUnFH5pJsLhYpWHITgVNUp8o+Ug9+2x+O4NOHxp5dAwNRB9VKhrZC2hPRc/OJ8hCgtlwJW8m/k/XphaHaUZU=',
        'argon2id');
