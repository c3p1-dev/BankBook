#ifndef CATEGORY_H
#define CATEGORY_H

#include <cstdint>
#include <optional>
#include <string>

class category
{
public:
    std::uint64_t id() const;
    void id(std::uint64_t category_id);

    std::optional<std::uint64_t> parent_id() const;
    void parent_id(std::optional<std::uint64_t> parent);

    const std::string& code() const;
    void code(std::string category_code);

    const std::string& name() const;
    void name(std::string category_name);

    const std::optional<std::string>& description() const;
    void description(std::optional<std::string> category_description);

private:
    std::uint64_t id_{0};
    std::optional<std::uint64_t> parent_id_;
    std::string code_;
    std::string name_;
    std::optional<std::string> description_;
};

#endif  // CATEGORY_H

/*
    SQL query to create the table

    CREATE TABLE IF NOT EXISTS categories (
        id          INTEGER PRIMARY KEY,
        parent_id   INTEGER REFERENCES categories(id) ON DELETE RESTRICT,
        code        TEXT NOT NULL UNIQUE CHECK (length(code) > 0),
        name        TEXT NOT NULL CHECK (length(name) > 0),
        description TEXT,
        CHECK (parent_id IS NULL OR parent_id <> id)
    );

    CREATE INDEX IF NOT EXISTS categories_parent ON categories(parent_id);
*/