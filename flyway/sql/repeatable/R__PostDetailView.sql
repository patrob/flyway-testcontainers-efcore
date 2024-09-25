CREATE OR REPLACE VIEW post_detail_view AS
SELECT p.id                                   AS post_id
     , p.title                                AS post_title
     , p.text                                 AS post_text
     , CONCAT(u.first_name, ' ', u.last_name) AS author
FROM posts p
         LEFT JOIN users u ON u.id = p.user_id