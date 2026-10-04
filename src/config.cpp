#include "config.h"

#include <filesystem>
#include <fstream>

// internal functions
namespace
{
std::string_view trim(std::string_view str)
{
    constexpr auto ws = " \t\n\r";

    const auto first = str.find_first_not_of(ws);
    if (first == std::string_view::npos)
        return {};

    const auto last = str.find_last_not_of(ws);
    return str.substr(first, last - first + 1);
}
}

// config class implementation
bool config::load(std::string_view filename)
{
    // open configuration file
    std::ifstream file{std::filesystem::path(filename)};
    if (!file)
        return false;

    // read line by line
    std::string line;
    while (std::getline(file, line))
    {
        // trim line and skip empty lines and comments
        const std::string_view view = trim(line);
        if (view.empty() || view.front() == '#')
            continue;

        // split line into key and value
        const auto pos = view.find('=');
        if (pos == std::string_view::npos)
            continue;

        const auto key = trim(view.substr(0, pos));
        if (key.empty())
            continue;

        // store key-value pair
        set(key, trim(view.substr(pos + 1)));
    }

    return true;
}

bool config::save(std::string_view filename) const
{
    // open configuration file
    std::ofstream file{std::filesystem::path(filename)};
    if (!file)
        return false;

    // write key-value pairs to file
    for (const auto& [key, value] : data_)
        file << key << " = " << value << std::endl;

    return file.good();
}

std::string config::get(std::string_view key) const
{
    // find key in data map
    const auto it = data_.find(std::string(key));
    if (it != data_.end())
        return it->second;

    // return empty string if key not found
    return {};
}

void config::set(std::string_view key, std::string_view value)
{
    data_[std::string(key)] = std::string(value);
}

std::map<std::string, std::string> config::get_all() const
{
    return data_;
}