#include "data/category.h"

#include <utility>

// category class implementation
std::uint64_t category::id() const
{
    return id_;
}
void category::id(std::uint64_t category_id)
{
    id_ = category_id;
}

std::optional<std::uint64_t> category::parent_id() const
{
    return parent_id_;
}
void category::parent_id(std::optional<std::uint64_t> parent)
{
    parent_id_ = parent;
}

const std::string& category::code() const
{
    return code_;
}
void category::code(std::string category_code)
{
    code_ = std::move(category_code);
}

const std::string& category::name() const
{
    return name_;
}
void category::name(std::string category_name)
{
    name_ = std::move(category_name);
}

const std::optional<std::string>& category::description() const
{
    return description_;
}
void category::description(std::optional<std::string> category_description)
{
    description_ = std::move(category_description);
}