#ifndef CONFIG_H
#define CONFIG_H

#include <map>
#include <string>
#include <string_view>

// configuration file parser class
class config
{
public:
    bool load(std::string_view filename);
    bool save(std::string_view filename) const;

    std::string get(std::string_view key) const;
    void set(std::string_view key, std::string_view value);
    std::map <std::string, std::string> get_all() const;

private:
    std::map<std::string, std::string> data_;
};

#endif