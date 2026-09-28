CXX      ?= c++
CXXFLAGS  = -O2 -Wall -std=c++20 -Iinclude -MMD -MP

OUT       = bankbook
SRCDIR    = src
OBJDIR    = build

SRCS      = $(wildcard $(SRCDIR)/*.cpp)
OBJS      = $(patsubst $(SRCDIR)/%.cpp,$(OBJDIR)/%.o,$(SRCS))

all: $(OUT)

$(OUT): $(OBJS)
	$(CXX) $(CXXFLAGS) -o $@ $(OBJS)

$(OBJDIR)/%.o: $(SRCDIR)/%.cpp | $(OBJDIR)
	$(CXX) $(CXXFLAGS) -c $< -o $@

$(OBJDIR):
	mkdir -p $@

-include $(OBJS:.o=.d)

clean:
	rm -rf $(OBJDIR) $(OUT)

.PHONY: all clean