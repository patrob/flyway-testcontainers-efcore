CREATE OR REPLACE VIEW user_post_stats_view AS
SELECT u.id                                   AS user_id
     , CONCAT(u.first_name, ' ', u.last_name) AS author
     , COUNT(p.id) AS posts_count
FROM posts p
         LEFT JOIN users u ON u.id = p.user_id
GROUP BY u.id, author