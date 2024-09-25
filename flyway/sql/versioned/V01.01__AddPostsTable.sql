CREATE TABLE posts
(
    id      int          NOT NULL GENERATED BY DEFAULT AS IDENTITY,
    user_id int          NOT NULL,
    title   VARCHAR(256) NOT NULL,
    text    VARCHAR(256) NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_user
        FOREIGN KEY (user_id)
            REFERENCES users (id)
);