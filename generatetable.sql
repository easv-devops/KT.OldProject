
DROP SCHEMA IF EXISTS webshop CASCADE;
CREATE SCHEMA IF NOT EXISTS webshop;
DROP TABLE IF EXISTS webshop.password_hash;
DROP TABLE IF EXISTS webshop.customer_buy;
DROP TABLE IF EXISTS webshop.avatar;
DROP TABLE IF EXISTS webshop.order;
DROP TABLE IF EXISTS webshop.users;

create table webshop.users
(
    user_id         SERIAL PRIMARY KEY,
    full_name  VARCHAR(50)  NOT NULL,
    email      VARCHAR(50)  NOT NULL UNIQUE,
    admin      VARCHAR(20)      NOT NULL DEFAULT 'Non-admin'
);

create table webshop.avatar
(
    avatar_id     SERIAL PRIMARY KEY,
    avatar_name  VARCHAR(50)  NOT NULL,
    avatar_price        integer   not NULL,
    information VARCHAR(300),
    deleted bool not null DEFAULT FALSE
);

INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Dwayne “The Rock” Johnson', 10, 'Dwayne Johnson, also known as “The Rock,” is a multifaceted celebrity. He’s a former professional wrestler, a successful actor, and a charismatic personality. With a strong presence on social media, Johnson engages with his fans regularly. He’s known for his roles in blockbuster films like “Fast & Furious” and “Jumanji.”', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Beyoncé', 10, 'Beyoncé Knowles-Carter is an iconic singer, songwriter, and actress. As one of the most influential musicians of our time, she has a dedicated global fan base known as the “Beyhive.” Beyoncé’s albums and live performances consistently receive critical acclaim.', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Cristiano Ronaldo', 10, 'Cristiano Ronaldo is one of the greatest football (soccer) players in history. His skills on the field and his social media presence make him a global sensation. Ronaldo has won numerous awards and holds various records in football.', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Kim Kardashian', 10, 'Kim Kardashian rose to fame through the reality TV show “Keeping Up with the Kardashians.” She’s also a businesswoman, model, and socialite. Kim’s influence extends to fashion, beauty, and lifestyle.', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Ellen DeGeneres', 10, 'Ellen DeGeneres is a beloved comedian, actress, and television host. Her talk show, “The Ellen DeGeneres Show,” has been a source of entertainment and inspiration for millions worldwide.', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Lionel Messi', 10, 'Lionel Messi, like Cristiano Ronaldo, is considered one of the greatest football players ever. He’s known for his incredible dribbling skills and goal-scoring abilities. Messi’s name is synonymous with excellence in football.', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Oprah Winfrey', 10, 'Oprah Winfrey, often referred to as the “Queen of All Media,” is a media mogul, talk show host, and philanthropist. Her influence in the entertainment industry and her charitable work have made her a global icon.', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Justin Bieber', 10, 'Justin Bieber gained fame as a teenager through his music. He’s known for hits like “Baby” and “Sorry.” Bieber’s fan base, known as “Beliebers,” remains dedicated to his music and career.', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Rihanna', 10, 'Rihanna is a Barbadian singer, actress, and fashion designer. Her music career has been immensely successful, and she’s also made a mark in the beauty and fashion industries with her Fenty brand.', false);
INSERT INTO webshop.avatar (avatar_name,avatar_price, information, deleted)
VALUES ('Kylie Jenner', 10, 'Kylie Jenner, part of the Kardashian-Jenner family, has risen to prominence as a businesswoman and social media influencer. She’s known for her cosmetics company, Kylie Cosmetics.', false);


create table webshop.order
(
    order_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES webshop.users (user_id)
);

create table webshop.customer_buy
(
    customer_buy_id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NUll,
    avatar_id INTEGER NOT NUll,
    FOREIGN KEY (order_id) REFERENCES webshop.order (order_id),
    FOREIGN KEY (avatar_id) REFERENCES webshop.avatar (avatar_id)
);

create table webshop.password_hash
(
    user_id   integer      NOT NULL,
    hash      VARCHAR(350) NOT NULL,
    salt      VARCHAR(180) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES webshop.users (user_id)
);